# Manual de previsão

> Tradução automática por italiano: em caso de dúvida vale o guia italiano.

A guia Previsão calcula a propagação tropoférica por si só: um mapa de índice na área visível e o sinal fornecido entre sua estação e um correspondente. Este manual descreve todos os dados, como é calculado e de que fonte vem. Os números entre parênteses rectos referem-se às fontes na parte inferior; no programa cada dado dos resultados tem um cômico (simbolo i) com a mesma fonte e a referência ao parágrafo.

## 1. O que ele faz e o que ele faz

A previsão dos perfis verticais de temperatura e umidade do modelo GFS [1, 2] deriva da reatividade da camada de ar por camada e estima quanto o sinal ganha em relação a uma atmosfera normal. Na rota adiciona o solo real (Copernicus GLO-30 [3]), difração, troposcatter, dutos e ruído receptor.

Não é uma medida: é uma estimativa feita em um modelo meteorológico com níveis distantes centenas de metros. Serve para comparar situações (hoje melhor do que ontem, 70 cm melhor do que 2 m, este tratamento melhor do que isso), para não garantir uma conexão. O parágrafo 14 explica como coletar evidências para validá-lo.


> "A propagação não pode ser generalizada, mas estudada em relação à morfologia local. »
> — Volker Grassmann, DF5AI, comunicado por Flavio Egano, IK3XTV (propagação em 2 metros)

É o princípio que orienta o cálculo ponto-ponto: cada rota é calculada em seu solo, com seus horizontes, seu mar e seus relevos; o mapa dá o quadro geral, trata a resposta.

## 2. Comandos de linguagem

- Banda: 144, 432 ou 1296 MHz. Mapa e cálculo usam a banda escolhida.
- Agora: o cursor move a previsão de agora até 72 horas à frente, dentro de 3 horas.
- Actualização da previsão (zona visível): baixar perfis GFS em uma grade de até 240 pontos cobrindo o mapa visível. O passo da grade (0,5° ou mais) depende da ampliação.
- Parar: interromper um download ou cálculo.
- Detalhes para mostrar (painel direito do mapa): três níveis, Essencial, Normal e Completa, ligue um grupo de caixas; cada caixa pode então mudar à mão. As caixas estão em três grupos:
  - No mapa: isolinee del gain, também 3-7 dB (tratamento), isobare, inversão térmica, sinais meteorológicos S ↓ ↑, ganho/pressão de correlação, zona de visão direta (parágrafos 4, 5, 7 e 9 bis). As cores ficam sempre.
  - No cálculo do sinal: todas as linhas do cálculo, perfil plano da Terra, log de cálculo e log de arquivo detalhado.
  - Correções e dados ao vivo: correções meteorológicas (Misme, subsidência), contraste ar-mar, média com pontos (corridoi), APRS 2 m saltos e LoRa, além da montanha sem perdas (parágrafos 4 bis, 9 bis, 9 quater e 10 bis).
  - Essential detém apenas isolinéus, clima e correções de mídia com pontos; Normal adiciona isobare, inversão, sinais meteorológicos, visão direta, terra plana, ar-mar e APRS; Completo liga-lo. O nível só muda o que você vê e as correções entram: os cálculos permanecem os mesmos.
- Fonte de dados e caso histórico: de onde vêm os perfis e que dia você olha (parágrafos 3 bis e 3 ter).
- Minha estação e o correspondente: Localizador, antenas, energia, cabos, ruído (parágrafos 7 e 12).
- Calcular o sinal e a cobertura em óptica: os dois cálculos no solo (pontos 8–13).
- Registro de cálculo: cada etapa com os números usados, também no arquivo Data/ forecast-calcoli.log.

## 3. De dados GFS para perfil

Para cada ponto da grade e a cada hora lemos a pressão no solo, temperatura e umidade a 2 m, o vento a 10 m, a altura da camada limite, o CAPE e temperatura e umidade em relação a 1000, 975, 950, 925, 900, 850, 800, 700, 600 e 500 hPa. De 1.3.0 você também pode ler o vento a 850 hPa e a velocidade vertical a 850 e 700 hPa (parágrafo 9 bis).

Os níveis são calculados com a fórmula ipsométrica e a temperatura virtual; os níveis abaixo do solo são escassos. Para cada nível:

- N = 77,6/T · (P + 4810·e/T), com T em kelvin, P e em hPa, e pela pressão de saturação de ITU-R P.453-14, eq. 9, com o fator de correção FE (recontrolado no texto oficial) [4].
- M = N + 157·h, com h em km: a reactividade modificada. Onde o M sobe há uma conduta.

## 3 bis. Fonte de dados

O menu Fonte de Dados escolhe de onde vêm os perfis:

- Automático (recomendado): GFS primeiro do NOMADS [14], o serviço NOAA que distribui o modelo: para cada momento o programa pede dois recortes da área no formato G RIB2 e lê-los por si só; NOMADS não tem limite diário, apenas um por minuto que o programa respeita. Com grades largas (de 0,5° para cima) ele usa 0,5° ou 1o arquivos a cada 3 horas, muito mais leve. Os pontos que o NOMADS não dá, ou todos se o NOMADS não responder, perguntam ao Open-Meteo [2]. As duas fontes dão o mesmo modelo e a mesma corrida, para que você possa misturar na mesma grade.
- GFS de Open-Meteo: apenas Open-Meteo, como em 1.2 (no limite por minuto é esperado).
- GFS de NOAA NOMADS: Apenas NOMADS. Mais lento para baixar (dois arquivos por hora, meio segundo entre um pedido e outro para não exceder o limite NOAA), mas não consome o limite Open-Meteo.
- ICON-EU (DWD) e ECMWF IFS 0,25°, de Open-Meteo: modelos diferentes do GFS, que se aplicam em toda a grade e para as rotas. Eles não se misturam com o GFS: comparar dois modelos ao mesmo tempo é uma maneira simples de ver como a previsão é incerta. ICON-UE cobre apenas a Europa; ECMWF não tem todos os níveis de pressão GFS (o salto de níveis em falta) e pode não ter a camada limite.

A fonte usada é escrita na linha de status e na primeira linha de resultados (fonte de dados), com quantos pontos chegaram de cada serviço e quantos do cache. A cache do disco é separada por modelo.

## 3 ter. Casos históricos

A Histórica O menu de caso revisa uma abertura documentada com as previsões do arquivo Open-Meteo [15] GFS: o mapa se move para a área do caso, a banda se configura e o cursor de tempo sai do primeiro dia do caso. Ao pressionar Update previne você baixar os perfis daqueles dias; o cálculo do sinal funciona como para a previsão atual. Serve para comparar o modelo com o que realmente aconteceu.

O primeiro caso é o de 14-15 de agosto de 2022, dito no QST (novembro de 2022, "The World Above 50 MHz" [23]): uma abertura sob uma alta pressão entre Manitoba e Kansas/Missouri, de 144 a 1296 MHz quase todos em FT8, com o registro canadense em 10 GHz de 1228,56 km em Q65 e sinaliza até 1941 km. A segunda é a abertura entre 29 de dezembro de 2021 e 2 de janeiro de 2022 sobre o Tirreno eo Mediterrâneo ocidental em 144 MHz, a partir das relações de IC8TEM: primeiras aberturas em 29 (até 737 km), mais ampla em 31 ( IT9GSF – EB5EA 1217 km), grande abertura em 1 de janeiro entre Itália, Sicília e Espanha. Para casos mais antigos (por exemplo, Setembro de 2000) o arquivo Open-Meteo não tem dados. No arquivo não há altura da camada limite: lá sua correção não se aplica.

## 4. O índice e as cores

Para cada camada entre dois níveis o programa compara a queda de N com os limiares da tabela 6.5 de Bean e Dutton [5]: por exemplo, entre 1000 e 850 hPa uma diminuição até 60 N/km é normal, mais de 100 N/km é super-refração. Uma camada super-refrativa vale de 10 a 15 dB de ganho; um conduíte que prende a banda de 20 a 25 dB, de acordo com o déficit de M. Vento forte para o solo, camada de limite alto e CAPE alto reduzem o ganho, pois removem ou quebram as camadas [5, § 4.4].

A cor é o ganho estimado em dB, com uma escala fixa igual em todos os temas:

| Ganho | Cor | Significado |
| 7 dB | Ninguém. | atmosfera quase normal |
| 7–10 dB | azul | gradiente sobre o normal |
| 10–13 dB | Verde | camada super-refractiva |
| 13–16 dB | amarelo | camada forte, mas sem canal para banda |
| 16-18,5 dB | laranja | camada muito forte ou ducto muito fino |
| 18,5–21 dB | vermelho | Banda de Trapagem de Condutos |
| a partir de 21 dB | Roxo | Dutos fortes e profundos |

Com a caixa de exibição também 3-7 dB zonas entre 3 e 7 dB, onde algo está sendo formado, mas permanece sob a escala, você pode ver com uma retenção cinza claro; cores não mudam. Um painel pontilhado delimita a área coberta por sua própria previsão e mostra o ganho máximo visível: se estiver abaixo de 7 dB o mapa permanece sem cores porque a atmosfera é quase normal, não porque os dados estão faltando. Ao baixar um novo clique na previsão de atualização não interrompe; para pará-lo, há Stop.

As cores são opacas para 82%, então o fundo do tema apenas as muda. A grade do localizador e nomes quadrados são projetados sobre as cores. Onde sua previsão não está lá, mas Hepburn ou F5LEN dar 5 ou mais cartas de xadrez aparecem com a cor dessa fonte.

O mapa julga cada ponto sozinho. O cálculo do sinal controla que a camada é contínua ao longo da rota (parágrafo 9): uma rota que cruza muito amarelo pode dar menos do que o mapa faz você pensar.

## 4 bis. Previsão mediada pelos comerciais: corredores (a partir de 1.3.3)

Política de Copyright: Um link sozinho nunca deve desenhar um conduíte. Uma recepção isolada e curta pode ser a dispersão de um avião em trânsito (o pacote APRS é muito curto), uma abertura de alguns minutos ou um erro, e daria "oleodutos gota" que eles enganam. O programa reconhece um corredor quando, na mesma banda:

- pelo menos 3 pares de estações independentes (sem estação partilhada) em rotas de, pelo menos, 300 km;
- As rotas são paralelas: azimut a ±15°, pontos médios a 150 km do eixo comum;
- todos nos últimos 30 minutos, com o feixe a repetir-se à distância de pelo menos 10 minutos (persistência);
- Para a ARS, um salto só conta se o mesmo salto for repetido;
- a partir de 1.3.4, as rotas de mais de 1500 km contam apenas se o feixe vier de pelo menos duas fontes diferentes (por exemplo, PSKReporter e um cluster): de uma única fonte, para essas distâncias em 2 m, são mais frequentemente EME, meteoro de dispersão ou localizadores errados do que tropo.
- A partir de 1.3.4, não contar as estações 'para raio': que na última hora tem pelo menos 5 correspondentes mais de 1000 km espalhados por uma ventoinha de pelo menos 150°. Em 2 m o trope é direcional; um receptor que "livre" juntos Israel, Portugal, Inglaterra e Itália de 2000-3700 km, muitas vezes com sinais positivos, tem quase certamente a frequência configurada mal. Na 17–22/11/2026 I5MZY arquivo 72% de alojamento em 2 m mais de 1000 km vieram de estações assim; em 22/11 às 10:53 UTC foram OH6HLH, OZ1ITS, F4LUK, OM8ABC e SP2JLR, e com o filtro o corredor mais longo desce de 2175 km (diretório 1.3.3) para 615 km.

Fontes: ponto de cluster (não AS/ACS), PSKReporter via MQTT, seu QSO e WSJT-X/JTDX/MSHV decodificações, e APRS salta em 2 m e LoRa 433/868 MHz.

APRS: 2 m e LoRa. O programa lê APRS-IS em somente leitura (rotate.aprs2.net, filtro na Europa) e extrai saltos de rádio entre estações de localização conhecidas (orgente → digipeater → IGate), como o mapa VHF de DXView e APRS PropView (RF-YVY, Apache 2.0). Três faixas valem: 2 m, LoRa 433 MHz (corredores de banda 432) e LoRa 868 MHz (corridoi que contam para 1296: uma conduta que traz os 868 MHz também as frequências mais altas). A banda vem do LoRa tocall (lista aprs-dispositivo) e das frequências ou palavras no comentário; um salto só vale entre duas estações da mesma banda.

O ponto delicado são os repetidores ativos: um sinal recebido a 500 km pode ser chegado com duas ou três retransmissões digipeater. Um salto conta, portanto, apenas se estiver entre dois transmissores reais e consecutivos:

- o caminho deve ser rastreado: cada digipeater que tenha retransmitido colocou seu nome, e o salto é medido entre um e o seguinte; um alias usado sem nome (W IDE1 *, W IDE2 *, alias regional) quebra a cadeia e esse trecho não conta;
- retransmissões ocultas: o programa lembra o caminho que cada fonte normalmente pede (por exemplo W IDE2 -2). Se um pacote vem com um salto em menos (W IDE2 -1) e nenhum nome o explica, um digipeater o retransmitiu sem rastrear e o pacote é descartado; as fontes nunca vistas antes não contam;
- digipeater no meio da estrada: se entre as duas estações, mais ou menos na rua, há um digipeater da mesma banda ativa nas últimas três horas, o salto é descartado, porque poderia ter retransmitido (é especialmente para o digipeater LoRa que retransmitir sem mudar o caminho);
- os pacotes não radioativos (qAC, qAX, TCPIP), aqueles direcionados da internet no rádio de um IGate (terceiras partes), HF e frequências diferentes, estações a altitude superior a 1500 m (palões, aviões) e saltos superiores a 1500 km.

Efeito no mapa (a partir de 1.3.4). Desenha apenas a área onde o mínimo garantido é válido: uma faixa de ±60 km em torno da parte central de cada rota (de 15% a 85% de seu comprimento), não nas estações nas extremidades, onde o conduíte pode não estar lá. As bandas de todos os corredores são desenhadas em conjunto, em uma cor neutra (o texto) e semi-transparente: as sobreposições não se somam, e a cor não confunde com o roxo da escala (21 dB e além). Os rótulos (traços, distância média, ganho mínimo) são digitalizados uns com os outros; passando com o mouse em um rótulo as seções do corredor aparecem.

Na área, e apenas perto do tempo atual (±90 minutos), a previsão não cai abaixo de um mínimo. O raciocínio é: se as estações realmente passarem, a propagação está lá, o que quer que o modelo meteorológico diga.

Porque as rotas curtas valem pouco (de 1.3.4). Em 2 m, entre 300 e 600 km, duas boas estações (cerca de cem watts, antenas, FT8 ou CW) conectam quase todos os dias mesmo com a atmosfera normal, para troposcatter: o sinal difuso pelas pequenas irregularidades do ar no volume comum dos dois feixes. Três ou quatro pontos a 500 km dizem que a banda funciona como de costume, não que haja uma abertura: GEKO Magazine n. 69 ( IU3QEZ) estima para o troposcatter com 2×10 elementos fluindo de cerca de 440 km em SSB QRP, 510 km em SSB com 50 W, 600 km em CW QRP e 640 km em SSB com 500 W (fontes, onze voltas). Apenas mais de 600-700 km o troposcatter não é mais suficiente e uma série de conexões paralelas realmente indica um conduíte ou camada. Até 1.3.3 um corredor de 450-600 km aumentou a previsão para 9-12 dB, e o mapa mostrou uma melhoria que não estava lá.

O valor inicial, portanto, vai com a distância média das rotas:

- até 400 km: 3 dB;
- de 400 a 700 km: sal de forma linear de 3 a 10 dB (500 km → 5,3 dB; 600 km → 7,7 dB);
- de 700 a 900 km: de 10 a 16 dB; mais de 900 km permanece a 16 dB.

Sob o 7 dB, que está sob o início da escala, o mínimo permanece que: o corredor é desenhado com seu rótulo, vê-se que a banda funciona, mas as cores do mapa não mudam (com a caixa também mostra 3-7 dB o cinza pontilhado). Acima de 7 dB o valor é pesado, metade e metade, com o número de rotas independentes (3 traços → 0,25; 6 ou mais → 1) e com a mediana S/N de relações (−20 dB → 0,5; 0 dB ou mais → 1; desconhecido, como em pontos de cluster, → 0,75), a partir de 7 dB. Exemplos: 3 rotas sem S/N em 500 km → 5,3 dB; 5 rotas em 615 km → 7,7 dB; 3 rotas sem S/N em 900 km → 11,5 dB; 3 rotas para −20 dB em 900 km → 10,4 dB; 6 rotas para 0 dB em 900 km → 16 dB.

A distância mínima permanece 300 km: elevando-o para 500 km também teria removido as verdadeiras aberturas de curto alcance (por exemplo, um conduíte no Tyrrhenian a 400 km, que é reconhecido pela previsão do tempo e sinais muito fortes mais do que a distância sozinho). No arquivo I5MZY, em 22/11/2026 às 10:53 UTC, com esta regra e o filtro de raios os corredores mais longos foram de 615 km (7,7 dB) e 540 km (6,3 dB); com as regras de 1,3,3 chegou a 2175 km e 16 dB.

Até 1.3.3 o mínimo era válido em todo o rectângulo do corredor e dependia apenas da distância (de 10 dB para 300 km para 16 dB para 900 km): com muitos corredores longos juntos (por exemplo em 22/11/2026, doze corredores até 2620 km) elevou quase toda a Europa para 16 dB.

As curvas de ganho já não desenham «gocce»: as ilhas menores de 220 km em ambas as direções permanecem coloridas mas sem curva fechada, a menos que um corredor as confirme. Desliga com a "Previsão mediada pelos pontos".

## 5. Linhas opcionais e comparação com pressão e inversão

Curvas de ganho: linhas em 7, 10, 13, 16, 18,5 e 21 dB, as bordas da escadaria, desenhadas no campo suavizadas com o método dos quadrados de marcha, com a etiqueta em dB.

Isobare: pressão ao nível do mar a cada 4 do mesmo GFS, à hora de previsão, com os centros H e L.

Inversão térmica: curvas cobertas com quanto ar a 925 ou 850 hPa é mais quente do que a 2 m, a +1, +3, +5 e +8 °C.

Sob os controles uma linha dá a concordância do ganho com a pressão e com o inverso na área visível: coeficiente de correlação de Pearson, −1 a +1. Os dutos são frequentemente nascidos em anticiclones (o ar que cai calores e forma o inverso), portanto um acordo positivo com a pressão é normal; mas dentro do mesmo anticiclone o ganho muda muito entre mar, costa e montanhas. O inverso sozinho não é suficiente: dobrar as ondas apenas se acima do ar é mesmo secador [5]. A linha serve para ver, por enquanto, qual das duas melhores explica as áreas coloridas. A mesma comparação está escrita no registro.

## 6. Potenciais gasodutos: duplo cálculo

Os níveis de GFS variam de 200 a 1000 m: uma inversão fina é espalhada e o modelo não vê o conduíte. Se entre dois níveis a diminuição de N exceder o normal (40 N/km), o ar é estável (gradiente térmico abaixo de 3 K/km) e acima é seco (taxa de umidade específica abaixo de 0,8), o programa considera um ducto potencial e calcula-o de duas maneiras:

- Cálculo A: todo o excesso concentrado gota em 50 m.
- Cálculo B: a mesma diminuição distribuída na espessura típica dos gasodutos dessa área, retirada dos mapas climáticos da ITU-R P.453 [4] (no solo ou elevada).

Para o déficit e para o comprimento de onda máximo aprisionado o menor valor é mantido: é uma estimativa prudente.

Dados ITU-R P.453 são © ITU e podem ser usados para fins pessoais e não comerciais citando a fonte, mas não podem ser redistribuídos: para isso não estou no pacote do programa. Para ativar o cálculo B, basta baixar os mapas P.453 da ITU na aba: o programa, após mostrar as condições de uso, baixar o arquivo dos pipelines do site ITU (elevated_and_surface_duct.zip) para uso pessoal do usuário, extrair os arquivos em Data/ P453 e ligar o cálculo sem reiniciar. Alternativamente, faça o download da página digital ITU-R Study Group 3 (itu.int, ITU-R Study Group 3, produtos digitais, P.453, dados de superfície e dados de dutos elevados) e coloque os sete S_OCCUR. TXT, S_THICK.TXT, S_STREN. TXT, E_OCCUR.TXT, E_base.T Sem os arquivos o programa usa apenas o cálculo A e escreve-o na aba e no registro. A razão entre o déficit A e a intensidade típica P.453 é anomalia: Quantas vezes o oleoduto de hoje é mais forte que o normal. P453

## 7. Estações e Localizadores

O proprietário pode ter 4, 6, 8 ou 10 caracteres. Com 8 ou 10 caracteres, a posição é precisa e a partilha do solo vem do Copernicus GLO-30: a altura da antena é suficiente do solo. Com 4 ou 6 caracteres o quadrado é muito grande: é melhor escrever a cota da antena no mar; se 0 o programa permanece a estimativa com o solo SRTM no centro do quadrado mais a altura do solo e sinaliza-lo nos resultados. Para o correspondente, a altura padrão é de 3 m do solo; com a quota de mar 0 é usado solo SRTM mais altura.

## 8. Terras, difração e troposcatter

O perfil da rota é amostrado no Copernicus GLO-30 a cada 30 m (mais de 120 km de largura). O raio equivalente da Terra é k = 157/(157 + ΔN), com ΔN o declínio de N no primeiro km dos perfis GFS ao longo da rota [6, 7].

Alívios próximos das estações (a partir de 1.3.2, critério do autor). P.452 paga o ângulo do horizonte linearmente, 0,573 dB por mrad: um alívio de algumas dezenas de metros a poucas centenas de metros da antena custa dezenas de dB, muito mais do que se observa. Por esta razão, os relevos a 20 km de cada estação podem somar até 6 dB: o ângulo do horizonte devido a eles é limitado ao dos relevos mais distantes mais 10,5 mrad. É válido para a sua estação e para o correspondente. A linha «Pertos relevos» diz quantos dBs decolaram.

Fonte de terra (a partir de 1.3.2). As quotas provêm do Copernicus DEM GLO-30 [3]: 1 segundo arco, o mar é 0 e a costa é clara. Até 1.3.1, o programa utilizava as caixas 'skadi' dos terrenos do AWS. Perto da costa, essas praças misturam o SSRTM com dados de fundo marinho de baixa resolução e espalham a parte da ilha no mar: em torno de Capri criar um anel fantasma de 80-100 m entre 0,5 e 1 km da costa. Em 21 de novembro de 2026 IC8TEM relatou um horizonte inexistente para o noroeste; a comparação ponto a ponto com Copérnico confirmou-o (94 m no mar noroeste de Marina Grande vs. 0). Esse falso horizonte, de +2,6°, custou cerca de 26 dB de troposcatter para as estações costeiras. Os skadi panes permanecem apenas como uma reserva quando Copérnico não responde. As molduras Copernicus pesam 15-30 MB e baixam uma vez; as de apenas mar não existem e valem 0.

- Espaço livre: Lfs = 92,44 + 20 log f(GHz) + 20 log d(km) [8].
- Difração: Método de Bullington no perfil real mais ITU-R P.452-16 § 4.2 [6] delta-Bullington correção de terra esférica, com ITU-R P.526 [9] perda esférica.
- Primeira zona de Fresnel: raio (λ·d1·d2/d); livre se o solo deixar pelo menos 0,6 do raio. A borda da faca [9] é mostrada para cada obstáculo.
- No desenho do perfil o raio direto é verde na visão óptica e vermelha se passa sob o solo ou sob a curvatura da terra; pontos vermelhos são os obstáculos que invadem a 1a área de Fresnel (linha pontilhada de púrpura). No mar o "ponto vermelho" na altitude 0 m não é uma montanha: é o mar que sobe para a curvatura. Passar com o mouse no raio ou em um ponto aparece um cômico com explicação e valores.
- Quando um alívio perto da antena fecha a direção (por exemplo, um horizonte de dezenas de graus), difração e troposcatter dão perdas de centenas de dB: acima de 300 dB o programa escreve "> 300 dB" e "direção fechada por um obstáculo vizinho", o sinal "< −230 dB" e as relações S/N "−60 dB". Nesse caso, é aconselhável olhar para «planta recomendada» e «Via monte».
- Troposcatter: ITU-R P.452-16 eq. 45 a 50% do tempo [6], com distância angular dos horizontes e N0 dos perfis.
- Sem tropo: soma dos poderes de difração e troposcatter, nunca sob o espaço livre.

## 8 bis. Troposcatter: quatro modelos e incerteza

Próximo ao P.452, o programa calcula o troposcatter com outros três modelos, assim como o programa TropoLink [16], e mostra sua diferença como incerteza:

- NBS TN101 § 9 [25] (a partir de 1.3.5), mediana: L = 30 log f − 20 log d + F(λd) − F0 + H0 + Lgp. F(λd) é a função de atenuação do anexo III nas suas formas analíticas (Ns 301) com a correcção para Ns, H0 a função de ganho de frequência de § 9.2: a 144 MHz, com antenas baixas em comprimentos de onda, a reflexão do solo apaga parcialmente o campo na parte inferior do volume comum e o sal de perda de 3-10 dB. Ns é o da hora perfis GFS, os horizontes e alturas eficazes vêm do perfil de Copernicus, Lgp = 0,07·e^(0,055(Gt+Gr))) é a perda de acoplamento de abertura. É a mesma forma que o ITM usa em seu interior, mas com a reatividade de hoje em vez da do clima. Ele testa em seções planas a 144 MHz com antenas a 20 m: dentro de 2-4 dB da ITM entre 150 e 900 km e cerca de 2 dB na forma simplificada 9.2b da TN101, que não tem H0.

- ITU-R P.617-5 [17] L = F + 22 log f + 35 log Δ + 17 log d + Lc, com f em MHz, ω em mrad, d em km, F = 0,18· N0 ·e^(−hs/7,35) - 0,23·ΔN e Lc = 0,07·e^(0,055(Gt+Gr)). N0 e ΔN são geralmente retirados dos mapas climáticos da ITU; aqui usamos a cada hora os perfis GFS ( N0 ·e^(−hs/7,35) é a reatividade ao solo, ΔN a diminuição no primeiro km), assim também o troposcatter segue o tempo.
- RSGB VHF/UHF Handbook Formula, DL2FZN [18]: L = Lfs + 24,44 + 4,283 ln f + 0,06656 d. A inclinação com a distância é quase igual à do PROPBER (0,067 dB/km).

Volume comum (de 1.3.1): é o ponto onde o raio das duas estações é cruzado, cada um inclinado para o seu ângulo de horizonte, em uma Terra de raio equivalente ae = k·6371 km. A partir do TX, o raio sobe em z = hTX + x·ωt + x2/(2·ae); A distância x = (hRX − hTX + d^ Com horizontes planos e antenas na mesma quota a base vale d2/(8·ae): cerca de 1,3 km a 300 km, 3,7 km a 500 km, 9,4 km a 800 km. Quanto maior o volume, mais o ar é seco e rarefeito e menos se espalha: por esta razão mais de 800-1000 km o troposcatter se esgota. Nos resultados estão a partilha e a posição da base, a reactividade, o gradiente e a humidade do GFS nessa altitude, e a distância equivalente de Bray (QST, Novembro 1961): cada horizonte extra ou menos vale cerca de 111 km de distância. Referência: apresentação "Over the horizonte in VHF" da revista IU3QEZ (GEKO Magazine n. 69).

A linha Troposcatter, outros modelos mostram as duas perdas e incertezas como metade da diferença entre o modelo mais otimista e o mais pessimista. São apenas comparações: a perda sem tropo permanece a de P.452.

## 8 ter. Troposcatter da tarde sobre os relevos (de 1.3.4)

IK3XTV, de Thiene ( JN55, com os Alpes a 10 km), trabalha em tardes de verão sul da Alemanha a 400–500 km, sem visão, com sinais fortes, mas com evanescência rápida e profunda; o autor atribuiu-os a "células convectivas alinhadas como um trem de lentes". A observação é confirmada; a explicação adotada no programa é outra, fisicamente mais firme.

À tarde, o sol agita o ar sobre os relevos e a camada limite sobe para 2-3 km. Em sua parte superior o ar úmido abaixo encontra o ar seco acima: lá a turbulência do índice de refração (o tamanho que governa o troposcatter) é o máximo. Quando esse topo atinge o volume comum da rota, que em 400–500 km é de cerca de 2–3 km, o troposcatter é reforçado. Isto explica o tempo da tarde, alta pressão (céu silencioso, aquecimento forte) e evanescência rápida, típico da difusão e não dos dutos. As células convectivas não funcionam como lentes: produzem turbulência, e é a turbulência no topo da camada de reformulação que espalha o sinal.

Consequências das previsões de leitura:

- na terra, a primeira tarde é o pior momento para as condutas baixas e camadas, mas não para o troposcatter além das montanhas: as duas coisas devem ser lido separadamente;
- o programa já possui os dois dados que servem, a altura da camada limite fornecida pelo modelo e a participação do volume comum da rota (registre "volume comum" nos resultados): Quando estão perto, um tratamento de montanha à tarde pode ir melhor do que o cálculo em atmosfera normal;
- a partir de 1.3.4 o cálculo ponto-ponto leva em consideração: se o volume comum está na terra, ao longo da rota há relevos de pelo menos 800 m, o sol está acima de 15° e a camada limite excede 800 m, e o topo da camada remodelando (modelo de taxa do solo + altura da camada limite) está dentro de 500 m da base do volume comum, o troposcatter ganha 3 dB; entre 500 e 1000 m o ganho linear cai para zero. A linha "Troposcatter tarde". Com neve ao longo da rota (pelo menos 10 cm no solo de acordo com o modelo, snow_profundity variable of Open-Meteo or SNOD of the GFS from NOMADS) o reforço não se aplica: na neve o solo permanece frio e a camada em forma não cresce. O valor de 3 dB é provisório e deve ser calibrado com ligações reais; desliga com correções meteorológicas.

Fontes: observações de F. Egano IK3XTV (Radio Rivista e www.qsl.net/ik3xtv, "Propagação em 2 metros", "Células Convectivas", "Troposcatter sobre os Alpes"), fontes § 13.

## 9. A camada fundadora ao longo da rota

É o coração do cálculo do sinal. Ao longo da rota, os perfis GFS são tomados a cada cerca de 25 km. Para cada camada de pressão, o estiramento mais longo em que a mesma camada está presente em pontos consecutivos; contagem como ducto apenas camadas grossas pelo menos 40 m [10].

- continuidade: a que km para que km, e a fracção da rota;
- topo e base, e inclinação em m/km;
- solo dentro da camada: o km em que as montanhas se elevam acima da base;
- Terra coberta: no mar o conduíte é quase sempre, decide o trecho de terra [10]. Se a terra exceder 10% da rota, a fração utilizada é o mínimo entre continuidade e cobertura da terra.

O ganho de trope é o ganho da camada para a fração utilizada. Se mais da metade dos perfis forem sub-refrativos, o ganho é negativo.

## 9 bis. Correções meteorológicas: estabilidade, subsídio, greve

A partir de 1.3.0 o perfil também fornece três informações meteorológicas, que podem ser vistas no registo (regave «estability (Misme)»), na linha do Meteo ao longo da rota dos resultados e, com a caixa do Meteo Segni, no mapa.

- Estabilidade do Misme [5, capítulo 6]. O misme liga o campo além do horizonte a um parâmetro composto: M = a·(ge − 40 + b·ΔW), com a = 0,5 dB por N/km, o gradiente equivalente ge e a estabilidade térmica ΔW, o trabalho para elevar o ar de 1 km. O programa calcula ΔW = g·∫(γ− ω0)/ω0 dz entre o solo e 1 km acima, com temperatura potencial de Δ; a atmosfera padrão é de cerca de 56 J/kg. O termo adicionado ao ganho é 0,07 dB para J/kg além do padrão, limitado entre −4 e +6 dB, e conta apenas onde nenhum conduíte prende a banda (o gradiente já está no índice Bean e Dutton). A calibração é temporária: deve ser verificada com as razões (ponto 14).
- Subsidence e uphill ar [19]. Moler e Holden, em rotas de 125-305 km para o mar em 1365 e 9380 MHz, encontraram o maior sinal de cerca de 2,5 dB em média com o ar descendo que com o ar subindo, e em um caso um reforço de cerca de 20 dB por 3 horas no início do subsídio, seguido por uma diminuição de 22 dB quando o ar voltou a convergir. O GFS dá a velocidade vertical para 850 e 700 hPa. Se em ambos os níveis o ar cair mais de 1 cm/s e houver uma queda inversa ou acentuada no índice de refração potencial K (N relatado adiabaticamente para 1000 hPa, que destaca o ar seco e quente caiu do topo [5]), 1,5 dB é adicionado; se o ar sobe mais de 5 cm/s se decolar 2.
- [20] Scirocco. Ar quente e seco do Saara sobre o ar marinho mais frio dá inversões muito fortes do Mediterrâneo oriental para Gibraltar. O sinal S aparece onde a 850 hPa o vento vem de 100-200° com pelo menos 7 m/s e a temperatura é de pelo menos 16 °C, no Mediterrâneo. É apenas uma indicação: o ganho decide as camadas do perfil.

Sem um canal que aprisiona a banda o ganho total não excede 16 dB, o final do amarelo: as correções tornam as camadas mais fortes mais evidentes, mas não as passam por dutos (vermelho e roxo permanecem nos dutos).

- Persistência (de 1.3.1). As maiores aberturas vêm após dias de alta pressão já bons (VHF/UHF DX Book; Biancomano e Leyshon 1998, regra de 12 horas; os relatórios de 29/12/2021–2/1/2022 crescem do primeiro ao quarto dia). Para cada célula, o programa conta as horas das 24 anteriores com ganho de pelo menos 13 dB e soma 2 dB em proporção, apenas quando o ganho já é de pelo menos 10 dB.

Como ler os sinais no mapa (Alterar sinais meteorológicos; desde 1.3.2 o símbolo é grande duplo e passar com o mouse sobre um sinal aparece um cômico com a explicação e os valores da célula):

| Assinar | Cor | O que significa isso? | Efeito no tropo |
|---|---|---|---|
|  ↓  | Verde | subsidência: gotas de ar (anticiclona), aquece para compressão e seca, fortalece a inversão e mantém firme | favorável, +1,5 dB |
|  ↑  | vermelho | ar ascendente (frente, baixa pressão, convecção): mistura camadas baixas e quebra inversões e condutas | desfavorável, −2 dB |
|  S  | laranja | Xarope: ar quente e seco do Saara sobre o ar fresco do mar | pista favorável, sem correção: decidir as camadas |

Uma área cheia de verde ↓ sob um anticiclone é o sinal clássico de uma abertura dura; Vermelho ↑ setas ao longo da linha dizem que as camadas estão quebrando, embora a cor da célula ainda é bom.

A caixa Correções Meteorológicas desliga essas correções para ver o modelo de 1.2.

Validação (ponto 14): nas 593 ligações a 144 MHz entre 29 de dezembro de 2021 e 2 de janeiro de 2022, em comparação com as mesmas rotas e duas semanas depois, o índice de separação (0,5 = nenhuma, 1 = perfeita) é 0,79 sem correções e 0,91 com correções (antes do teto a 16 dB). A mesma comparação mostra o limite principal: o conduto no mar em baixa altitude, que naqueles dias trazia os sinais entre Campânia e Ligúria, não é visível nos níveis do GFS.

## 9 ter. Notícias 1.3.2: duas estimativas, tecto, alta pressão, mar, estações, cumes

estimativa prudente e agressiva. O ganho do gradiente cresce de 0 dB até o limite de normal até 10 dB até o limiar de superrrefração. Na camada 1000–850, o limiar prudente é Bean e Dutton (100 N/km); o limiar agressivo é o limiar clássico de super-refração de 79 N/km (k 2; P.453 usa −100 N/km nos primeiros 100 m), que com k50 = 157/(157 – ΔN) é k 2 (k = 2,5 corresponde a cerca de 94 N/km). O cálculo ponto-ponto mostra as duas perdas ladeadas; com um conduíte que aprisiona a banda coincide. O mapa tem cuidado.

Só se procura o sótão de 2500 m. Estradas e dutos até 2500 m acima do solo: quanto mais altas as antenas de rádios amadores não as exploram. Com 1500 m a validação perdeu um ponto de separação e 4% das conexões acima de 16 dB, pois no inverno as reversão de subsidência são muitas vezes entre 1500 e 2500 m.

Alta pressão estendida. +1 dB em que pelo menos 7 das 9 células em redor têm uma pressão reduzida para o mar igual ou superior a 1020 hPa e o vento é fraco nas camadas baixas (10 m abaixo de 5 m/s, 850 hPa abaixo de 8 m/s), excepto para movimentos ascendentes.

Peso do mar. No cálculo ponto-ponto, o ganho das camadas é multiplicado por 0,8 + 0,4·μ1, onde μ1 é o fator de ITU-R P.452 (eq. 51) no maior trecho contínuo de terra: 1 em mar aberto, pequeno com longos trechos de terra.

Postos e conduzidos. Quando um conduíte que aprisiona a banda cobre apenas parte da rota, a parte de cada antena é comparada com a base e a parte superior do conduíte no início e no final da seção coberta; o ganho é multiplicado por 0,5 + 0,5·?(e^(−ΔhTX/300)·e^(−ΔhRX/300)), com Δh a distância em metros fora do ducto. Com o conduíte em quase toda a rota é o acoplamento do § 10.

Camada e cumes. Se o topo da camada limite do GFS (taxa de terreno da maioria do modelo limite_camada_altura) exceder os cumes da rota (previsões máximas acima de 300 m, se o mais alto exceder 500 m), a camada passa sobre as montanhas sem quebrar: +1,5 dB para a fração de cumes cobertos. É a observação de IT9GSF nas aberturas da Sicília para a Alemanha com Tyrrhenian favorável.

Níveis próximos ao solo no mar. Com a fonte NOMADS o programa também baixa a temperatura da superfície (no mar é o SST), temperatura e umidade a 80 m e na camada 30-0 hPa sobre o solo. Nos pontos de mar (taxa-modelo inferior a 5 m) estes níveis são adicionados ao perfil, sob o primeiro nível de pressão, para ver o ducto entre 100 e 200 m que os níveis de pressão não resolvem. No solo não são utilizados: as camadas da noite e do dia ainda não são válidas. Open-Meteo só dá a temperatura da superfície.

Oleoduto de evaporação. Do SST, da temperatura e umidade para 2 m é estimada a altura δ do tubo de evaporação com o perfil logarítmico neutro (como no modelo de Paulus e Jeske, sem a correção da estabilidade): o ar para o cabelo da água está saturado para o SST (98% para o sal), portanto,

```math
\delta = \frac{N_{mare} - N_{2m}}{0,157\,\ln(2/z_0)}, \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

limitado entre 0 e 40 m. Para capturar 144 MHz seria necessário cerca de 180 m (Kerr), então em 144 MHz o ducto de evaporação reforça pouco; de 432 MHz para cima, com antenas baixas no mar, muito (parágrafo 11 bis). Digite a equação parabólica e na linha Correções 1.3.2.

Hora do dia (visita). O programa calcula a altura do sol em cada ponto. De dia a dia na terra, com o sol sobre 15° e camada limite sobre 800 m ou vento sobre 5 m/s, adverte que os dutos de terra são remodelados; à noite, com sol abaixo de −3°, vento abaixo de 3 m/s e camada limite abaixo de 300 m, adverte que com céu sereno são possíveis conduzidos por irradiação, que cai após o nascer do sol. Não no mar. Por enquanto são vistos, não números: A validação de dezembro não tem dias de verão suficientes para calibrar. São dois pontos que o autor considera importantes, a partir da observação diária.

Perfil normal CRPL (alternativo). Em vez de 40 N/km fixo a diminuição normal segue a atmosfera de referência exponencial CRPL: N(h) = Ns·e^(−c·h), com −ΔN no primeiro km = 7,32·e^(0,005577·Ns) (Bean e Thayer 1959). Com ar úmido (Ns alto) o normal é mais íngreme e serve um gradiente mais forte para dar ganho; em altitude é mais doce. Parece mau até que a validação diga que ajuda.

## 9 quater. Contraste ar-mar e mares baixos: o Adriático (a partir de 1.3.3)

As ondas de rádio não sentem o fundo do mar: sentem o contraste entre ar e superfície da água. A profundidade conta porque decide quão rapidamente a temperatura do mar segue a do ar.

O Adriático é um mar baixo. A profundidade média é de cerca de 250 m, mas a bacia do norte raramente excede 100 m e o Golfo de Trieste permanece abaixo de 25 m; apenas ao sul desce mais de 1200 m. Uma camada de água tão fina aquece e esfria em poucos dias, o tirreno em semanas. Existem três situações típicas:

1. Primavera e início do verão, favorável. O mar ainda é frio, mesmo para a água doce dos rios Alpino e Po; chega ar quente e seco da planície ou dos Balcãs. Ar quente sobre a água fria forma uma inversão no mar (oleoduto de advecção): é a condição clássica das aberturas entre Itália, Eslovénia e Croácia.
2. Outono e inverno com bora, desfavorável. Ar frio acima de um mar mais quente: a camada baixa torna-se instável, pressiona e o ducto quebra.
3. Depois de uma bora, oportunidade. Só porque o mar é baixo, uma bora esfria alguns graus em poucos dias. Se você entrar em um anticiclone com ar quente, o contraste ar-mar é forte e o ducto é mais robusto do que o habitual.

O Adriático é também um canal fechado entre Apeninos e Dinari, com menos de 200 km de largura: a camada do mar permanece confinada, e as rotas ao longo do eixo (Puglia-Venezia-Trieste, cerca de 700 km) permanecem todas no mar, a melhor condição para um ducto longo.

O que o programa calcula. Nos pontos de mar, com as correções meteorológicas e a caixa «Contrasto aria-mare»:

| Condição | Correcção |
| --- | --- |
| temperatura potencial a 925 hPa (ou 950) sobre o SST de 4 K | _ |
| para além do SST de 7 K | +2 dB |
| com humidade do ar a 925 hPa (mais de 80%) | −0,5 dB nestes bónus |
| mar refrigerado de 1,5 °C ou mais em 72 horas antes, com contraste favorável | _ |
| mar ar quente a 2 m de 2 °C (mistura) | −1,5 dB |
| mar quente de 4 °C | −3 dB |

A correção total permanece entre −3 e +3 dB e, sem conduíte, o ganho permanece sob o teto amarelo. Os limiares são temporários: na atmosfera padrão a temperatura potencial a 925 hPa excede a do mar de cerca de 2-3 K, então 4 K já indica um inverso.

A memória da SST. Os modelos mantêm a temperatura do mar quase estável durante a previsão. A diminuição recente é derivada da análise dos três dias anteriores (Open-Meteo, apenas temperatura de superfície, apenas células marinhas): máximo de 72 horas antes menos o valor atual. Se o serviço não responder, o bônus de memória não se aplica.

Limites. O SST dos modelos abaixo da costa e no Adriático superior é grosseiro; a caneta fria do Po, por exemplo, é mal resolvida. A Batimetria não entra no cálculo: as fontes dão sempre o SST, que já resume o efeito do fundo do mar. O contraste ar-mar se sobrepõe em parte no final da estabilidade de Misme (§ 9 bis); para isso os bônus são pequenos. Os limiares irão tarate com os corredores do ponto e do QSO através do Adriático.

## 10. Realizado em toda a rota

Se a camada aprisionar a banda em pelo menos 80% da fração utilizada e as estações não estiverem à vista, a perda na conduta é também calculada: espaço livre, mais o acoplamento das antenas (3 dB se estiverem dentro da conduta, mais se estiverem acima ou abaixo), mais a atenuação residual γd = 5·10−5·ae·f^(1/3) dB/mrad de ITU-R P.452 eq. 49 [6]. Se for inferior à perda sem tropo torna-se a perda esperada.

Uma banda permanece no conduíte somente se seu comprimento de onda não exceder λmax = 2?2·(2/3)·?(ΔM·10−6)·d (Kerr, eq. 17 [11]). Para esta mesma camada pode prender os 23 cm e 70 cm, mas não os 2 m. No entanto, o limite não é líquido: Kerr adverte que mesmo ondas várias vezes mais tempo sofrem muito do conduíte, e Hepburn e o livro VHF/UHF DX falam de bordas "esferográficas" e dutos "perdoáveis". A partir de 1.3.1 o programa utiliza uma conduta suave: se λmax estiver entre λmax e 3·λmax, o ganho passa gradualmente de 15 dB (λ = 3·λmax) para o ganho total da conduta (λ = λmax), com o factor s = (λmax/λ − 1/3)/(2/3). Nesse caso, as cores do ducto são permitidas. O tronco e a linha Meteo ao longo da rota indicam os pontos com ducto macio.

## 10 bis. Via Monte

Quando o correspondente está fechado atrás do horizonte, o programa busca a melhor direção para apontar as antenas: a inclinação iluminada espalha o sinal como alvo de radar. Faz o círculo do horizonte a cada 5o (exceto ±15° em torno do correspondente) e em cada direção leva o ponto iluminado do horizonte, mais de 100 m e entre 1 e 60 km. Uma estimativa rápida (espaço livre, conversão bitáctica e 10 dB para cada nível de horizonte do difusor para o correspondente) escolhe os 4 mais promissores, pelo menos 15° uns dos outros; para cada:

- tratar 1, estação → alívio, no espaço livre;
- trata 2, relevo → correspondente, calculado como se o relevo transmitido de 10 m acima do topo, com a mesma previsão (sem equação parabólica);
- Conversão com a equação do radar bistático, com σ = σ0·A, σ0 = −15 dB (montanha em VHF/UHF) e A a área iluminada pelo feixe R1 (máximo 20 km2):

```math
L_{via} = L_{fs}(R_1) + L_2 + 10\log_{10}\frac{\lambda^2}{4\pi\,\sigma_0 A}
```

A linha "Ponto Recomendado" indica o azimute da melhor rota, se custar menos que o direto. Em 144 MHz, com um alívio de 20 km e um yagi de 15°, a estrada de montanha custa cerca de 45 dB mais do que a 2 rota por si só: só acontece quando o direto é fechado por obstáculos vizinhos. A estimativa é bruta (±10 dB), especialmente para σ0.


Sem perda para a montanha (opção, de 1.3.2). Com a caixa «Via monte senza perdite fino al monte», a queda entre a estação e o ponto de difração é considerada nula: é como levar a estação para o topo ( antena a 10 m acima do ponto, mesma potência, antena e cabo). O horizonte então escolhe o ponto com o horizonte mais livre para o correspondente (em paridade, o mais alto) e o resultado mostra a rota do ponto para o correspondente, com sinal e S/N, próximo ao total com a difusão. É um limite otimista: diz se a direção abre quando a montanha levanta bem o sinal, ou onde ir portátil. Cada ponto é dado com o dez caracteres.

## 10 ter. Entrada para uma conduta distante

Se o conduíte que aprisiona a banda começa longe, além do horizonte da estação, o sinal deve chegar. O programa traz o sinal para o ponto de partida, no meio do conduto, para difração: ao longo do perfil de Copérnico GLO-30 procura a borda com o parâmetro Fresnel mais alto (ITU-R P.526), com o inchaço do terreno equivalente,

```math
\nu = h\,\sqrt{\frac{2}{\lambda}\left(\frac{1}{d_1} + \frac{1}{d_2}\right)}, \qquad J(\nu) = 6,9 + 20\log_{10}\left(\sqrt{(\nu - 0,1)^2 + 1} + \nu - 0,1\right)
```

mais atenuação devido à curvatura do topo (obstáculo arredondado, P.526-16 § 4.2), com o raio R estimado do solo de Copérnico GLO-30 até ±1 km da borda,

```math
T(m, n) = 7,2\,m^{1/2} - (2 - 12,5\,n)\,m + 3,6\,m^{3/2} - 0,8\,m^2, \qquad m = R\,\frac{d_1 + d_2}{d_1 d_2}\left(\frac{\pi R}{\lambda}\right)^{-1/3}, \qquad n = h\,\frac{(\pi R/\lambda)^{2/3}}{R}
```

(para mn > 4 é o formulário 34b da recomendação). Barsis (NBS, 1963) medido em três longas rotas além de uma montanha (Colorado, Alasca, Europa) perdas médias de 10 a 20 dB acima da borda ideal de Fresnel-Kirchhoff: o arredondamento explica muito (Barsis e Kirby, 1961: Pico Pikes com um raio de cerca de 11,5 km). O resultado se compara com a difração em solo esférico liso; é o pior. Em seguida, o sinal viaja na conduta: espaço livre em toda a rota, atenuação residual γd de P.452 na seção na conduta e acoplamento do correspondente (3 dB dentro, 1 dB a cada 10 m fora). Se esta perda for inferior à perda esperada, torna-se o mecanismo ("conduta (entrada por difração)"). É premissa do autor sobre geografia italiana: com um obstáculo próximo bem abaixo da linha, v é negativo e a entrada custa pouco; com uma frente dorsal custa 15-30 dB.

## 10 quater. ITM (Longley-Rice) e saída da conduta

A partir de 1.3.2 o programa contém o Modelo de Terraim Irregular 1.2.2 da NTIA/ITS (Longley and Rice, ESSA ERL 79-ITS 67, 1968; Hufford, Longley and Kissick, NTIA 82-100, 1982), traduzido para C# pelo código oficial do governo dos EUA, que está livre de direitos. A tradução dá os mesmos números que o original: auto compilação compara-o cada vez com NTIA C++ código em cinco casos de teste (baixo abaixo de 0,01 dB).

O ITM calcula a perda em atmosfera normal no perfil real Copernicus GLO-30: visto com reflexão do solo, difração (espigol e terra esférica pesando sobre a rugosidade do solo Δh, ou seja, a diferença entre o percentil 10 e 90° das quotas em torno da reta dos quadrados mínimos) e troposcatter. A reatividade do solo é reduzida para a porção média da rota, $N_s = N_0\,e^{-h/9460}$, e dá a curvatura do terreno equivalente,

```math
\gamma_e = 157\cdot10^{-9}\left(1 - 0,04665\,e^{N_s/179,3}
ight)\ \mathrm{m}^{-1}
```

O programa utiliza o clima de rádio da rota escolhida como no § 10 sexies (até 1.3.4 sempre o mar temperado, com N0 320 na terra e 350 no mar), com N0 a partir da reatividade média do lugar (solo εr 15, σ 0,005 S/m; mar εr 81, σ 5 S/m), polarização horizontal e variabilidade ao longo do tempo sem o de lugar (o perfil é conhecido). Mostra a perda excedida em 50% do tempo e a que você tem em 10% e 1% do melhor tempo. O ITM não modela os dutos: sua variabilidade ao longo do tempo é estatística, pelo clima. Ele serve como o segundo cálculo da linha de base ao lado de P.452; a diferença entre os dois dá uma ideia de incerteza.

O segundo uso é o trecho final após um conduíte que termina antes do correspondente. No ponto de saída, uma antena virtual é colocada a meio caminho do conduto e o ITM calcula o trato para o correspondente, como excesso sobre o espaço livre em 50% do tempo. Perdas totais

```math
L_{uscita} = L_{fs}(d) + A_{TX} + \gamma_d\,	heta_{condotto} + A_{ITM}(d_{uscita} 	o RX)
```

onde A_TX é o acoplamento da antena (3 dB dentro, 1 dB a cada 10 m fora) se o conduíte começar próximo, caso contrário a difração de entrada de § 10 ter. É o limite que o autor se deu: continuidade da camada mais, no máximo, atenuação residual. Se for inferior à perda esperada, torna-se o mecanismo ("conduta (saída e estiramento final ITM)").

## 10 quinquies. Aprofundamento: ilhas, estreitos e descontinuidade terra-mar

Um exemplo do Golfo de Nápoles (Anacapri, JN70CN) para o Mediterrâneo ocidental mostra onde as rotas cruzam as ilhas:

| Destino | Azimut | Onde atravessa as ilhas | Obstáculo |
| --- | --- | --- | --- |
| Barcelona |  279°  | Norte da Sardenha (Gallura) até 41,05° N, entre 420 e 490 km | cerca de 30 km ao sul da Bocche di Bonifacio; a Limbara (1359 m) a cerca de 20 km da linha |
| Bocche di Bonifacio |  282°  | perto de 41,3°N | cerca de 11 km |
| Marselha |  296°  | Córsega Central a 42,2° N | montanhas até 2700 m: uma conduta abaixo de 2 km interrompe |
| Valência, Baleares |  260–265°  | Sardenha Central | alívios internos |

Para Barcelona é suficiente apontar para 282° em vez de 279° porque o eixo passa das bocas: 3o, dentro do feixe de um yagi normal.

A boca é um estreito? Para os dutos baixos, abaixo de algumas centenas de metros, sim: as ilhas são muradas, e acima da terra, no dia, o aquecimento quebra a inversão. O estreito em si é suficientemente largo: cerca de 11 km, contra uma primeira área de Fresnel, com menos de um quilómetro de largura metade de uma rota de 1000 km em 144 MHz. O problema é o tempo. Os Bocas são uma diversão para o vento: A camada inferior é acelerada e a camada inferior é removida. Muitas vezes, a passagem é aberta como geometria, mas perturbada como atmosfera. Com um anticiclone estável e pouco vento pode em vez disso ser a boa passagem.

Disparidade terra-mar. Eles contam. Um conduíte formado na costa tirrênica é interrompido: o sinal sai do conduíte (a seção após a saída, § 10 quater) e deve retornar para outro conduíte no mar das Baleares, pagando novamente o acoplamento. Os conduítes em altitude, acima de 1500 m, passam sobre a Gallura sem perceber; para Córsega central não se aplica.

Na prática: se a rota cruza uma ilha montanhosa, tentar algum grau de correção para o trecho de mar livre, mas não contar com o caminho com vento forte no estreito.

## 10 sexies. Variação do tempo e clima de rádio TN101 (a partir de 1.3.5)

O TN101 § 10 [25] descreve como a perda de uma rota para além do horizonte varia ao longo do longo período em torno da mediana, com diferentes curvas por clima: a função Y(q) diz quantos dB a perda cai no q% do melhor tempo. O ITM contém essas curvas para sete climas. O clima conta muito: no mar no clima marinho temperado, a 144 MHz, em 10% do tempo você ganha muito mais dB do que na terra no clima continental, porque os dutos são mais frequentes.

O programa já não corrige o clima: Ele escolhe-o para todos os gostos do ponto médio, por isso funciona em todo o mundo e não apenas na Europa. Baixar do Open-Meteo [26] três anos de valores diários da reanálise ERA5 (temperatura, umidade e pressão do solo, chuva) para a célula de 0,5° do ponto médio, uma vez apenas; os valores permanecem em Dados/CLIMA. Restauração do solo

```math
N_s = \frac{77{,}6}{T}\left(P + 4810\,\frac{e}{T}\right)
```

dia a dia, então a média anual e excursão entre médias mensais, umidade média e chuva anual. As regras seguem as descrições dos climas do TN101 § 10.2; as fronteiras entre um clima e outro são critério do autor:

- mais de 60° de latitude: continental temperado (o TN101 não tem curvas para o clima polar);
- menos de 250 mm de chuva por ano e humidade média inferior a 50%: deserto (Ns típicos 280);
- a 20° do equador, Ns de pelo menos 350 e excursão até 30: equatorial (Ns típicos 360);
- até 32° e uma excursão de, pelo menos, 60: continental subtropical, ou seja, monções (excursão típica 60–100);
- entre 10° e 32° com Ns de, pelo menos, 340: marinho subtropical (Ns típicos 370);
- ambos os horizontes no mar, mais de 20°: marinho temperado no mar (é a definição do TN101);
- excursão até 30 perto do mar (mar na rota ou umidade média de pelo menos 75%): temperada marítima na terra;
- de outro modo temperado continental (excursão típica 20–40).

N0 para ITM é a média reduzida ao nível do mar, N0 = Ns·e^(h/9460). Sem rede você usa uma regra de reserva com a latitude sozinho e a fração do mar, e a linha diz isso.

Testes com dados ERA5 2023-2025: Capri Ns 339 com excursão 45 (temperatura continental em terra, temperada no mar quando ambos os horizontes estão no mar); Londres 327 e 16 Viena 319 e 24 Abidjan 381 e 11 (equatorial); Miami 365 e 36 (marido subtropical); Delhi 337 e 82 (conconcontinental subtropical); Riad 278 e 37, 90 mm chuva (deserto). No Chile: Antofagasta 341 e 19 (marido subtropical: a costa do Atacama, com o reverso baixo quase fixo do Pacífico), Calama 232 no chão, 2257 m, 14 mm de chuva (deserto), Valparaíso 331 e 11 (temperatura), Punta Arenas 313 e 5 (temperatura).

A linha "Clima de rádio ( TN101)" mostra o clima, a razão, Ns com metade da excursão e Y: quantos dBs ganham em 10% e 1% do tempo e quantos perdem em 90%. É estatísticas de longo prazo e permanece separado da previsão: a perda esperada segue o tempo de hoje com os perfis GFS, e somar as duas coisas contariam duas vezes a mesma propagação anormal.

## 11. Comparação com ITU-R P.452

A linha P.452 aplica o método ITU para dutos e reflexão sobre camadas (P.452-16 § 4.5 [6]) com a mediana ΔN do GFS em vez do climatológico e p = 20% como constante de calibração, como no estudo CSIRO [10]. Trata-se apenas de uma comparação: a perda esperada continua a ser a do ponto 9.

## 11 bis. Equação parabólica (de 1.3.1)

A Linha de Equação Parabólica é um quarto cálculo, apenas de comparação, feito como programas profissionais para os ductos (PETOOL; o framework de propagação de onda de M. Lytaev, licença MIT): em vez de estimar o ganho das camadas, avança a onda ao longo da rota com o método de Fourier split-step dentro do perfil de refração GFS M modificado, retirado do ponto de previsão mais próximo de cada distância. Os níveis de GFS adicionam os saltos estimados dos dutos potenciais (calculando A, 50 m na meia camada). O solo Copérnico GLO-30 entra em etapas (sob o solo o campo é nulo), o solo é condutor e o feixe da antena é um gaussiano com a abertura estimada a partir do ganho. O cálculo vê os conduítes, aqueles que perdem, a difração e o acoplamento das antenas; Ele não vê o troposcatter, que é adicionado separadamente. Em seguida, o «cheque de perfil»: o mesmo com atmosfera padrão menos aquele com perfil esperado.

De 1.3.2:

- O propagador está em ângulo largo, com a fase exacta do espaço livre:

```math
\hat{u}(x + \Delta x, p) = \hat{u}(x, p)\, e^{\, i \Delta x \left( \sqrt{k^2 - p^2} - k \right)}
```

- o fundo do mar e a planície costeira a altitude zero reflectem com o coeficiente de Fresnel na polarização horizontal, εc = εr − j·60·λ·σ (mar εr = 81 e σ = 5 S/m; país 13 e 0,006 S/m), no ângulo claro da rota:

```math
\Gamma_h = \frac{\sin\psi - \sqrt{\varepsilon_c - \cos^2\psi}}{\sin\psi + \sqrt{\varepsilon_c - \cos^2\psi}}
```

- a rugosidade do mar reduz a reflexão consistente de acordo com Miller e Brown, com a altura efetiva das ondas do vento para 10 m (σh = 0,0051·U2):

```math
\rho = \exp\left[-2\left(k\,\sigma_h \sin\psi\right)^2\right]
```

- no mar, o perfil M inclui a conduta de evaporação (ponto 9 ter) com o perfil logarítmico neutro:

```math
M(z) = M_s + 0,125\left[(z - z_s) - \delta\,\ln\frac{z + z_0}{z_s + z_0}\right], \qquad z_0 = 1,5\times10^{-4}\ \mathrm{m}
```

Ele testa com o protótipo (10 m de antena no mar, ganho na atmosfera padrão em 100-300 km): com δ = 10–30 m a conduta de evaporação dá 1–3 dB a 144 MHz, 8–37 dB a 432 MHz e 21–66 dB a 1296 MHz. Em 144 MHz em polarização horizontal, o mar reflete quase como um metal, e a rugosidade conta pouco: o novo fundo muda alguns décimos de dB; conta mais de 432 MHz para cima.

Limites: acima da terra o perfil vertical permanece o dos níveis de GFS, de modo que os dutos finos permanecem invisíveis; o solo a passos é aproximado; o feixe permanece dentro do 6o.

## 12. antenas, sinal e ruído

- Yagi DL6WU: ganho 9.5 + 7.8·log10(boom/λ) dBd (2,35 dB para duplo boom, DL6WU medidas em VHF Comunicações 3/1982), mais 2,15 para [12] dBi.
- EIRP = potência − cabo + ganho TX.
- Sinal recebido = EIRP - perda esperada + ganho RX - cabo RX. Medidor S IARU: S9 = −93 dBm, 6 dBm por unidade.
- Ruído externo de acordo com ITU-R P.372 [13]: galáctico (Fa = 52 − 23 log f) mais artificial de acordo com o ambiente (cidade, residencial, rural, rural tranquila) ou uma temperatura de antena escrita à mão.
- Tsys = Ta + Tpre + [290(Lc − 1) + 290(Fr − 1) ·Lc] / Gpre para os terminais da antena (fórmula Friis). A partir de 1.3,5 a estação receptora sempre possui um pré-amplificador na antena, parâmetro fixo: ruído número 0,8 dB (Tpre = 59 K) e ganho 20 dB. Atrás estão o cabo (perda Lc) e o rádio, com ruído básico número Fr 6 dB. Com 1 dB de cabo o cabo e entrada de rádio cai de cerca de 1160 K para 12 K: o ruído decide o céu, o ambiente e o pré-amplificador. O ganho de 20 dB é um valor típico escolhido pelo programa.
- S/N em SSB, CW e FT8 na banda de cada forma, em comparação com o conjunto limiar.
- Probabilidade para o período (a partir de 1.3.2): a evanescência dentro da hora é uma distribuição normal do S/N em torno do valor horário médio esperado. Bray (QST, novembro 1961, p. 39, sobre as curvas de Norton 1959) dá uma amplitude de 13,5 dB sobre cerca de 100 milhas, ou seja, cerca de 7 dB abaixo e 7 acima da média, que cai quase reta abaixo de 100 milhas (3,5 dB para 50 milhas); para uma ligação 'sólido' leva 7 dB do orçamento. Tomado ±7 dB como ±2σ: σ = 3,4 dB sobre 160 km, proporcional à distância abaixo. A probabilidade de um período exceder o limiar do caminho é

```math
P = \Phi\left(\frac{S/N - S/N_{soglia}}{\sigma}\right)
```

  com δ função de distribuição normal: margem 0 dB = 50%, +3,4 dB = 84%, +7 dB (a margem 'sólida' de Bray) = 98%. Não inclui a incerteza do modelo, que é maior.
- Formas digitais: S/N em 2500 Hz em comparação com os limiares medidos a 50% de decodificação, sem informação prévia (AP). FT8 −21 dB e FT4 −17,5 dB no ruído branco; quando o mecanismo é troposcatter usar os limiares com evanescência "distúrbio do meio de latitude", FT8 −18,6 dB e FT4 −15,2 dB [21]. Q65 -15C −22,2 dB, Q65 -30B −24,8 dB, Q65 -60A −27,6 dB Na aeronave de dispersão o Doppler pode exceder os tons de Q65 -60A (1,7 Hz): há melhor que 30B ou 15C. JT65A −25 dB [22]. A decodificação AP (nome do já conhecido ou fim do QSO) dá cerca de 2 dB extra para FT8 e 1,5 a 2,6 dB para Q65; não entra no limiar porque não é válido para todos Os valores «−24 dB» que são frequentemente lidos para FT8 são o limite de relações WSJT-X ou decodificação AP, não o limite em 50%. [24]

## 13. Resposta e tempos de dados

Cada cálculo mede seu próprio tempo: perfis GFS (rede, análise, quebras obrigatórias de 20 s entre os lotes para o limite Open-Meteo), terreno Copernicus GLO-30 (com o download dos painéis, 15-30 MB cada, apenas a primeira vez), cálculo. A linha de tempo de resposta resume-os; no registro as linhas começam com TEMPI. Ao atualizar a grade, o registro também relata a corrida GFS usada, quando ela se tornou disponível e qual a idade dos dados.

A partir de 1.3.2 você também pode escolher ICON-2I, o modelo da Agência ItaliaMeteo e Arpae na Itália em cerca de 2,2 km, através do Open-Meteo (modelo italia_meteo_arpae_icon_2i, duas corridas por dia, 3 dias). Abrange apenas a Itália e os mares vizinhos e tem níveis menos baixos do GFS (1000, 925, 850, 700, 500 hPa): para os conduítes em altitude não é melhor do que o GFS, mas a grade fina descreve bem costas, ilhas e mar em 2 m, que é o ducto de evaporação. As variáveis que um modelo não tem (limite de velocidade, temperatura da superfície...) removem-se da solicitação. A partir de 1.3.2 a fonte automática só usa NOMADS; Open-Meteo serve se NOMADS não responder e para casos históricos. Com a caixa "Atualizar por conta própria" (iluminada por padrão) a previsão será baixada sem clicar: na abertura, em cada nova corrida do GFS no NOMADS (cerca de 5 horas após a hora da corrida) e quando a área visível estiver fora por mais de um quarto da grade; no máximo uma tentativa a cada 15 minutos, nunca com um caso histórico.

Os pedidos no Open-Meteo têm limites por minuto, por hora e por dia para cada endereço de internet. Os perfis baixados são salvos no disco (Dati/cache-gfs) para cada traço e modelo: com a mesma corrida, que muda a cada 6 horas, os pontos já baixados não perguntam novamente. Com a fonte automática você começa a partir de NOMADS e Open-Meteo só serve para pontos faltando; com o único Open-Meteo, no limite de tempo ou diariamente pára imediatamente e diz quando tentar novamente. Casos históricos não são guardados em cache.

## 14. Validar o modelo

A previsão deve ser comparada com conexões reais. O registro de cálculo mantém cada número; o arquivo do programa mantém anúncios e decodificações. Para 432 e 1296 MHz, onde os pontos são poucos, eles precisam de relatórios dos operadores: quem fez a conexão, a que horas, com que sinal. Compare por muitas horas o ganho esperado com o sinal medido diz se os limiares devem ser movidos.

## Fontes

1. NOAA/NCEP, Global Forecast System (GFS), modelo global a 0,25°, quatro corridas por dia.
2. Open-Meteo, prediction API (open-meteo.com), modelo gfs_seamless, variáveis em níveis de pressão e metadados de corrida.
3. Copernicus DEM GLO-30 (ESA; WorldDEM © DLR e.V. 2010–2014 e © Airbus Defence and Space GmbH 2014–2018, fornecido como parte do Copernicus pela União Europeia e pela ESA), 1 segundo arco, quadros GeoTIFF no registo aberto dos Web Services da Amazon. Reserva: NASA/USGS Copernicus GLO-30 nas telhas terrestres "skadi" da AWS.
4. ITU-R P.453, O índice de refração de rádio: sua fórmula e data de refração, com mapas de canais digitais (Grupo de Estudos 3, © ITU, uso pessoal e não comercial).
5. B. R. Bean, E. J. Dutton, Radio Meteorology, NBS Monograph 92, 1966: Tabela 6.5 e § 4.4.
6. ITU-R P.452-16, Procedimentos de previsão para a avaliação da interferência entre estações na superfície da Terra em frequências acima de 0,1 GHz: § 4.2 (difração), eq. 45 (troposcatter), § 4.5 e eq. 49 (conduzido).
7. ITU-R P.834, Efeitos da refração troposférica na propagação de ondas de rádio.
8. ITU-R P.525, Cálculo da atenuação do espaço livre.
9. ITU-R P.526, Propagação por difração: eq. 31 (cutcheon) e § 3 (terra esférica).
10. B. Indermuehle, H. Suzuki, Tróposférico Ducting Prediction baseado em dados do modelo GFS em Inyarrimanha Ilgari Bundara, o Observatório de Radioastronomia CSIRO Murchison, Publicações da Sociedade Astronômica da Austrália, 2026 (arXiv 2608.12861).
11. D. E. Kerr, Propagation of Short Radio Waves, MIT Radiation Laboratory Series vol. 13, 1951: eq. 17 e 18.
12. G. Hoch DL6WU, «Extremely Long Yagi Antenas», VHF Communications 3/1982, pp. 130 e seguintes, fig. 1 (medidas até 432 e 1296 MHz de acordo com o comprimento da explosão).
13. ITU-R P.372, Ruído de rádio: ruído galáctico e artificial para o ambiente.
14. NOAA/NCEP, NOMADS, filtro GRIB do GFS 0,25° de tempo (nomads.ncep.noa.gov); formato WMO G RIB2, embalagem complexa com diferenças espaciais.
15. Open-Meteo, Previsão histórica API (modelo de arquivo de previsão desde 2021).
16. LukiBox, TropoLink, projeto de rotas troposcatter com P.617, NBS TN101 e ITM flanqueados (GitHub, licença Apache 2.0).
17. ITU-R P.617-5, técnicas de previsão de propagação e dados necessários para o projeto de sistemas de rádio-relay trans-horizontal, 2019.
18. RSGB, Manual VHF/UHF; Implementação do DL2FZN (andi-f/troposcatter no GitHub, 2013).
19. W. F. Moler, D. B. Holden, «Divulgação de dispersão troposférica e circulação atmosférica», Journal of Research of the NBS 64D, 1960, doi 10.6028/jres.064d.014: sinal além do horizonte (1365 e 9380 MHz, no mar) e movimento vertical do ar.
20. ARRL Handbook 2022 (99a ed.), § 19.4.3-19.4.4: propagação meteorológica e troposférica.
21. S. Franke, B. Somerville, J. Taylor, The FT4 and FT8 Communication Protocols, QEX julho/agosto 2020.
22. W3SZ, limiares de modos digitais lentos ( JT65, JT9, QRA64) e escolha de modo de acordo com a propagação, 2019.
23. QST, "The World Above 50 MHz", novembro de 2022: abertura de 14 a 15 de agosto de 2022 entre Manitoba e Kansas/Missouri.
24. J. Taylor K1JT, B. Somerville G4WJS, S. Franke K9AN, N. Palermo IV3NWV, Guia de Iniciação Rápida para Q65, 3 de abril de 2021 (wsjt.sourceforge.io).
25. P. L. Rice, A. G. Longley, K. A. Norton, A. P. Barsis, Transmission Loss Predictions for Tropospheric Communication Circuits, NBS Technical Note 101, revista 1967: § 9 (troposcatter, function F(ωd), H0), § 10 e Anexo III (climas de rádio e variabilidade ao longo do tempo).
26. Open-Meteo, Historical Weather API (revisão ECMWF ERA5 desde 1940; Copernicus Climate Change Service, licença CC BY 4.0).
