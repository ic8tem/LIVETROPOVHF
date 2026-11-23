LIVETROPOVHF · Documento técnico 2
# Construção do mapa

> Tradução automática do italiano: em caso de dúvida, vale o guia italiano.

Como nasce o mapa da aba Mapa e local: a base geográfica, as projeções, a média dos índices Hepburn e F5LEN, o isobare, os pontos e as estações de escuta, sobrepostas com camadas. Versão de 9 de novembro de 2026.

## Em resumo

- O mapa é projetado pelo programa em oito camadas, cada uma com sua caixa [8].
- Base: litoral natural da Terra e fronteiras em dois níveis de detalhe [9]; Mercado [10] e projeções Azimuth.
- Física: inversão, reatividade N e gradiente dN/dh, limiar de conduíte a −157 N/km [3] [4] [7] [11].
- Índice Trumpet: leitura calibrada dos mapas Hepburn [1] e F5LEN [2], grade comum e média das duas fontes.
- Isobare: pressão GFS [12] via Open-Meteo [15], linhas a cada 4 hPa, H e L centros.
- Spot, estações de escuta (círculos rosa escuros com linhas rosa sempre visíveis) e decodificações pessoais em vermelho.
- Leitura da sobreposição: alta pressão, dutos altos, queda de pressão, crescendo SNR, [3] [4] [5] [13] [14] rotas de montanha.

## 1. O princípio: um mapa em camadas

O mapa não é uma imagem fixa: é projetado pelo programa, camada sobre camada, cada vez que você muda zoom, projeção, banda, tempo de previsão ou tema. Cada camada tem sua caixa na barra de camadas superior no mapa, então você só pode segurar o que você precisa e ver tudo junto quando você quiser a imagem completa. A ordem, de baixo para cima, é esta:

|  #  | Estrada | Conteúdo | Casella |
|  1  | Mapa básico | Mar, terras, costas e fronteiras | Sim. |
|  2  | Tropo índice | Media Hepburn + F5LEN, ou apenas uma fonte; transparência ajustável | Sim. |
|  3  | Localização da grelha | Campos, quadrados e, com zoom, quadrados | Sim. |
|  4  | Isobare | Linhas de pressão a cada 4 hPa, centros H e L | Sim. |
|  4a  | Temperatura 850 hPa | Isotérmica tratada em cada 4 °C | Sim. |
|  4b  | Inversão térmica | Amostra laranja onde o ar em altitude é mais quente do que no solo | Sim. |
|  5  | Relatório PSK | Estações de recepção de PSKReporter (círculos rosa escuros) e, em rosa, o que eles ouvem | Sim. |
|  6  | Decodificado por mim | Estações de decodificação com WSJT-X, JTDX ou MSHV: linhas vermelhas do seu QTH | Sim. |
|  6a  | QSO desunti dal live | Ligações fechadas ( RR73, 73, RRR) lidas em suas decodificações: linha verde entre as duas estações | Sim. |
|  7  | Destaque | Conexões: linha azul cheia, amarelo coberto para airscatter, duas bolas | Sim. |
|  7a  | Exclusões | Casella Excluido a menos de 100 km: o tráfego mais curto não está desenhado nem listado. Casella Mais de 1000 km: apenas quadrados (começando): se uma das estações estiver a mais de 1000 km do seu QTH, sem linhas ou círculos, apenas o localizador de quadrados das estações envolvidas no lilás realizada a 45° | Sim. |
|  8  | QTH e rótulos | Sua estação, os nomes do local selecionado | sempre visível |

O índice de tropo está imediatamente acima da base e abaixo de tudo mais: é uma amostra colorida, e maior colocado cobriria linhas e símbolos. A grade passa por cima dele, então você sempre sabe onde uma área colorida cai. Os isobares são linhas finas e são bem lidos sobre a amostragem.

## 2. O mapa básico

Custos, fronteiras e terra vêm de dados de vetores naturais da Terra, de domínio público [9], para escala 1:50 milhões para vista geral e 1:10 milhões quando ampliada. Os arquivos estão na pasta do programa: sem serviços de mapas online. Cores do tema Limpar: mar azul claro # E2ECF8, costa azul escuro, bordas cinza claro. Nos temas Preto e Noite as cores da base são escurecidas, nos outros temas eles tinturam ligeiramente.

## 3. As duas projecções

Todas as camadas passam pela mesma função de projeção, por isso estão sempre alinhadas umas com as outras. Você escolhe entre duas projeções [10] (latitude λ, γ latitude, raio R da Terra):

Mercado. Armazenar cantos e formas locais; As distâncias se estendem em direção aos pólos. Corte para ±85° de latitude.

```math
x = R \cdot (\lambda - \lambda_{0})
y = R \cdot \ln \tan \left( \frac{\pi}{4} + \frac{\varphi}{2} \right)
```

Azimutale equidtanti centrado no seu quadrado. O centro é o centro da sua praça WWL. Distâncias e direções medidas do centro são verdadeiras: uma estação a 1200 km é 1200 km de escala, e o ângulo com o norte é o azimute para o qual apontar a antena. Quando você muda o localizador, o mapa é reorientado.

```math
\cos c = \sin \varphi_{0} \sin \varphi + \cos \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0})
k = \frac{c}{\sin c}
x = R \cdot k \cdot \cos \varphi \sin (\lambda - \lambda_{0})
y = R \cdot k \cdot \left[ \cos \varphi_{0} \sin \varphi - \sin \varphi_{0} \cos \varphi \cos (\lambda - \lambda_{0}) \right]
```

No azimute as linhas que no papel são retas (meridianos, paralelos, bordas quadradas) tornam-se curvas. Por esta razão, todas as linhas são divididas em seções curtas, cada seção é projetada e, em seguida, as características são unidas; o mesmo se aplica às bordas do índice de tropo e para o isobare. O ponto antipod no centro não tem posição definida e permanece fora de vista.

## 4. Localizador de grelha, zoom e movimento

A grelha Maidenhead é projetada pelo programa: campos de 20° × 10° com as duas letras grandes e desbotadas no centro, quadrados de 2° × 1o, e com zoom subquadrati suficiente de 5′ × 2,5′. As linhas são azuis e finas, não para cobrir o índice de tropo.

Zoom com a roda centrada no ponteiro (o ponto abaixo do mouse permanece estável), arrastando, duplo clique para voltar para toda a visão, + e − teclas, ea chave com a porcentagem para toda a visão. Detalhes adequados ao zoom: costa mais fina, sub-quadrados, etiquetas de isobare mais espessas.

## 5. Por que as inversões dobram as ondas

Na atmosfera baixa, a temperatura normalmente cai cerca de 6,5 °C por km; O índice de refração depende da temperatura e da umidade [3], e onde ela muda acentuadamente com a altitude a onda se curva para o chão e chega mais longe do que o normal. A camada mais importante é que até cerca de 3 km, onde inversões, dutos e células convectivas [4] são formadas; os caminhos no mar são favorecidos porque as massas de água formam grandes áreas de inversão [4]. [3]

O tamanho utilizado para mensurá-lo é N refração, ou seja (n − 1) · 106 [11]:

```math
N = \frac{77,6}{T} \left( P + 4810 \cdot \frac{e}{T} \right)
```

T é a temperatura em kelvin, P a pressão e pressão de vapor, ambos em hPa.

Conta como N variado com ação (dN/dh). Na atmosfera padrão é aproximadamente −39 N/km; até −79 N/km a refração é considerada normal, além de super-refração, e com gradientes positivos de sub-refração [7]. Sob −157 N/km a onda é dobrada mais do que a curvatura da terra e permanece presa: é o ducto [11] (ducting). Estas são as condições que os mapas de Hepburn e F5LEN tentam prever.

## 6. O índice tropo: de dois mapas para a média

As abas Hepburn e F5LEN são a mesa de controle: mostrar as imagens baixadas da página de cada fonte, com o tempo de previsão mutável, e servir para calibrar a leitura clicando nos pontos de referência e cores da legenda. A camada índice tropo permanece vazia até que pelo menos uma das duas calibrações não esteja ativa e completa.

Hepburn. O índice de Hepburn mede quantas ondas se espera que sejam dobradas sobre uma área, numa escala linear de 0 a 10, que também pode exceder 10. 0 significa atmosfera padrão e apenas dispersão; 1–2 superrrefracção marginal ou fraca; 3–4 superrrefracção com áreas de condutas; O índice não leva em conta o tropo de dispersão e não vê as montanhas [1]. Os mapas cobrem 6 dias: passes de 3 horas para as primeiras 36 horas, depois 6 horas, atualizados todos os dias para 18 UTC [1]; os dados iniciais (temperatura, umidade, pressão) vêm do modelo global do serviço meteorológico canadense [1]. [1]

F5LEN. Para a Europa F5LEN publica 31 mapas de +3 a +192 horas (a cada 3 horas até +48, a cada 6 até +96, então a cada 12), com a data e hora válidas no título, o tempo de produção na parte inferior direita e uma legenda de classe: Perturbação, Normal, Menor, Fraco, Bom, Forte, Vy Strong, Duct [2]. A previsão mundial do F5LEN é calculada pelo índice de refração do ar na Unidade N, em 40 passos de 3 horas (cerca de 5 dias), atualizado 4 vezes ao dia; um clique no mapa mostra o valor médio e o localizador desse ponto [2].

A prosseguir. Para cada passo de previsão, o programa executa estes passos:

1. Quitação. As imagens das duas previsões são baixadas na cache ao lado do executável, uma vez que para atualizar a fonte, não para qualquer redesenho. Antes da versão final, deve ser solicitada permissão para Hepburn e F5LEN.
2. Georreferenciamento automático. O programa encontra a moldura (duas linhas horizontais e duas verticais da mesma cor) e, no Windows 10/11, lê com o OCR integrado no sistema as etiquetas de eixos (54N, 10W, 0, 5E...). Cortar as tiras em torno do quadro, ampliá-las, testar ambos os sinais quando a letra N/S/E/W não ler e manter a linha em que mais rótulos cair: as leituras erradas permanecem para fora. A partir daqui você pode obter o lat/lon extremo da moldura e o tipo de projeção (gralha regular ou Mercator). Os extremos salvam-se e releem-se se o quadro mudar de tamanho. No Mac e Linux, sem OCR, extremos de fábrica ou escritos à mão são válidos na aba. Alternativamente, para mapas sem eixos legíveis: Cada tipo de mapa (Hepburn Europa, F5LEN mundial...) tem uma calibração: alguns pontos de controle com coordenadas conhecidas (cruzamentos de meridianos e paralelos, costa reconhecível) conectam pixels à latitude e longitude, na projeção utilizada por essa fonte. A calibração está em um arquivo ao lado do programa e você pode refazer se uma fonte mudar o formato.
3. Leitura colorida. Cada pixel é comparado com a legenda de origem e convertido no valor numérico mais próximo. Custos, escritos e grades desenhados no mapa original são reconhecidos como “sem dados” e preenchidos com os valores próximos.
4. Grade mundial. Cada imagem, uma vez que você encontra o quadro e nota seus extremos, é colocada na Terra; o valor em qualquer ponto do mundo é derivado das imagens que a cobrem. Se mais regiões cobrirem o mesmo ponto, vale a pena detalhar (mais pixels por grau), desde que seu tempo esteja a 1,5 h dessa escolha; então a hora mais próxima, então a corrida mais recente. Quando nenhuma imagem cobre o ponto, o mapa mostra um véu cinzento: nenhum dado, que é diferente do “índice 0”. Dentro da moldura, um pixel sem uma cor de lenda é 0 (a atmosfera normal); costas, bordas e escritos são pulados olhando para a cor mais próxima dentro de 3 pixels.
5. A mesma escada. Hepburn já está na escala 0-10 [1]: fundo escuro = 0 e dez cores em ordem (violeta, azul, água verde, limão, amarelo, ocre, laranja, vermelho, rosa, rosa claro) para 1...10, lilás além. F5LEN usa classes (Perturbação, Normal, Menor, Fraco, Bom, Forte, Vy Strong, Duct) [2], mas alguns têm exatamente cores Hepburn: Fraco = 1, Forte = 5, Vy Strong = 7, Duct = 8; Menor é 0,5 e Bom (verde) 3,5. O multiplicador de escala, na aba F5LEN, corrige o conjunto para torná-lo comparável com Hepburn.
6. À mesma hora. A barra de tempo vai de +0 a +192 h em etapas de 3 horas. Para cada passo você dá de cada fonte a previsão válida na hora mais próxima; Se uma fonte não tem essa etapa, a mídia só usa a outra e a legenda indica.
7. Mídia. Com o botão Mídia vale cada ponto (Hepburn + F5LEN) / 2; com Hepburn ou F5LEN você vê apenas uma fonte, útil para entender onde as duas previsões discordam.
8. Desenho. O valor é colorido com a escala de legenda (0 azul, 2 verde, 4 amarelo, 6 laranja, 8 vermelho, 10 roxo) e projetado como uma amostragem sob a grade. O 0 é transparente, então onde nada acontece você vê o mapa básico limpo; transparência geral ajusta com o cursor ao lado da caixa.

Na aba local, o índice ao longo da rota é a média dos valores da grade campeonatos a cada 10 km no círculo máximo entre as duas estações.

## 7. Isobare

As altas pressões estáveis são o primeiro sinal a ser procurado para a tropa [3], e as conexões além dos Alpes IK3XTV analisados foram com alta pressão [4].

1. Dados. Pressão ao nível do mar do modelo global GFS do NOAA [12], leia através do serviço Open-Meteo [15] gratuito numa grelha de 3° × 3° (de 30° a 72° N, de 30° W a 48° E), durante as 78 horas seguintes. O programa atualiza os dados na inicialização e depois a cada 6 horas, como o modelo de corrida.
2. Linhas. A isobare vem da grade a cada 4 hPa (...1012, 1016, 1020...) com o algoritmo dos quadrados marchando (quadrados marching), em seguida, eles são ligeiramente arredondados e projetado desenhado por área.
3. Centros. Os máximos e mínimos locais mais marcados são marcados com H (azul) e L (vermelho) e o valor em hPa.
4. Etiquetas. O valor aparece ao longo de cada isobara, em um fundo de cor do mapa para permanecer legível acima da amostragem.

Zona e passo. Os dados são baixados na área que você está olhando, expandidos em 25%, com um passo de 1o a 20o escolhido para ficar abaixo de 400 pontos (Open-Meteo conta cada ponto como uma solicitação: 600 por minuto, 5000 por hora): ampliando ou movendo o mapa para fora da área já baixado o programa o reload mais denso, no máximo a cada 10 minutos; se uma atualização falha os dados anteriores permanecem e ele vai tentar novamente após 3 minutos. Desta forma, isobare e temperaturas funcionam em qualquer lugar do mundo.

Temperaturas. O mesmo pedido traz a temperatura para 2 m, para 925 hPa e para 850 hPa. LA0BY mostra que o evento de 22/11/2018 a UA6 foi visto em mapas de temperatura (900-3000 m), mas não em previsões Hepburn [3]; DF5AI e DK5YA usam perfis de temperatura verticais para reconhecer inversões [5]. Para isso existem duas camadas:

- Temperatura 850 hPa: isoterme tratar cada 4 °C (vermelho acima de zero, azul abaixo, roxo sobre zero).
- Inversão térmica: amostragem em laranja onde o ar a 925 ou 850 hPa é mais quente do que aquele a 2 m; quanto mais intensa, mais forte é a inversão. É a condição física que dobra as ondas para o solo [3] [11], e compará-lo com o índice de tropo e isobare mostra se as previsões têm uma base meteorológica.

## 8. Pontos e estações

Spot. Cada ponto com ambas as posições conhecidas é uma linha no círculo máximo entre as duas estações, dividida em seções e projetadas; Em Mercatore aparece então ligeiramente curvado, no azimutale é reta se começa a partir do centro. Linha azul para o ponto normal, amarela coberta com borda escura para o airscatter (<AS>, <ACS> ou “airscatter” no comentário), uma bola para cada estação, halo claro sob a linha para separá-lo da amostragem. O local selecionado é mais frequentemente projetado para o último e com os dois nomes.

A ouvir. Cada Relatório PSK A estação receptora [16] é um círculo vazio rosa escuro. De cada início linhas cor-de-rosa completas para as estações que descodificaram e enviou de volta na rede, sempre visível sem clicar, com os nomes sempre escritos. Ao selecionar um círculo suas linhas são mais grossas. Spot e audição só mostram a faixa e o intervalo escolhidos na lista: mudando a largura de banda o mapa está vazio e redesenhado com os dados já coletados para a nova banda.

Decidido por mim. Cada estação que decodifica com WSJT-X, JTDX ou MSHV (receito via UDP) é uma linha vermelha cheia do seu QTH para a estação, com bola e nomeação. É a comparação mais direta com a previsão: mostra onde seu receptor realmente vem agora.

## 8 bis. Cores e símbolos

Cada cor tem apenas um significado, igual em todos os temas (alterar apenas o fundo).

| Cor e sinal | Significado |
| Linha azul contínua | Spot from DX cluster, RBN ou DM4X entre duas estações |
| Linha amarela com borda escura | Spot with airscatter comment (reflexão sobre a aeronave) |
| Círculo vazio rosa escuro | Estação de escuta no PSKReporter (MQTT) |
| Linhas rosa finas do círculo, com uma bola rosa no fundo | Estações que o ouvinte ouviu, com o nome sempre escrito; linhas mais grossas para o ouvinte selecionado |
| Linha vermelha e nome vermelho | Suas decodificações UDP (WSJT-X, JTDX, MSHV): Quem és tu? |
| Linha vermelha piscando traços | QSO em progresso com essa estação: Ele ligou para você e o fechamento ainda não chegou, ou é o DX Call of WSJT-X/JTDX com a transmissão habilitada. Continuar com RR73, RRR ou 73 |
| Linha verde | QSO desunti dal live: fechando RR73, RRR ou 73 UDP view |
| Linha preta com borda branca | Alguém está chamando você: dura até RR73/RRR /73 ou até 3 minutos |
| Quadrados de 45° | Tráfego com pelo menos uma estação além da coroa escolhida: Sem linhas, apenas localizador de quadrados |
| Procure lilas com distância para o norte | 1000, 700 e 500 km em torno de seu QTH; essa escolha é mais marcada |
| Círculo e ponto na cor do tema | Seu QTH, com nome e proprietário |
| Bola laranja com GRAVES 143.050 | O radar grava ( JN27SI) |
| Amostra de azul para vermelho | Índice de tropo de Hepburn (0–10) ou classe F5LEN, ou sua média; cor mais quente, melhores condições |
| Veludo cinzento claro | Área sem dados de tropo para o tempo escolhido |
| Linhas azuis-cinzentas com o valor em hPa | Isobere a cada 4 hPa; H em azul alta pressão, L em vermelho baixa pressão |
| Linhas pontilhadas de vermelho, azul e roxo | Isotermia a 850 hPa a cada 4 °C: vermelho acima de zero, azul abaixo de zero, roxo o zero |
| Amostra de laranja | Reversão térmica: ar para 925 hPa mais quente do que no solo, mais intenso com inversão mais forte |

Nos Relatórios suspeitos a borda de cada aba diz a regra em vigor: verde Bom, vermelho Scarta, cinza nenhuma regra.

Quais são os quadrados lilás cobertos a 45°? O tráfego longe de seu QTH lhe interessa entender onde a propagação está aberta, mas suas linhas, centenas de quilômetros de comprimento e muitas vezes dezenas, cobrir o mapa e esconder o que conta para você: as aberturas que você pode explorar de sua estação. Por esta razão, além da coroa escolhida, o programa não desenha as linhas, mas coloriza os quadrados localizadores (2° × 1°) das estações envolvidas. O resultado é um mapa de «zonas activas»: onde aparecem muitos quadrados lilás próximos, há um canal ou uma abertura, mesmo que não lhe diga directamente respeito; Se a área se alarga em sua direção, é o sinal para apontar a antena nessa direcção. O painel de 45° permite que você veja sob o índice tropo e a grade, então compare de relance a previsão (cor inferior) e o tráfego real (illus). Seu quadrado QTH nunca colore, porque é o ponto de partida e não uma área distante. Apenas os quadrados que estão todos além da coroa são coloridos: um quadrado atravessado pela coroa, ou por dentro, permanece livre, mesmo que haja uma das duas estações. Para ver as linhas, basta desligar a caixa ou escolher uma coroa maior.

Os critérios que decidem o que vem no mapa e o que é descartado são explicados um por um no documento 1, seção 11 ter.

## 9. A barra de tempo

Abaixo no mapa: data e hora da previsão mostrada, cursor de +0 a +72 h às 6 horas, legenda de índice 0-10 com o nome da fonte escolhida, e tempo da corrida modelo para isobare. O índice Trumpet e isobare seguem o cursor; manchas, estações de escuta e suas decodificações são observações do momento e sempre permanecem aqueles “adex”. Quando o cursor não está em +0 eles são atenuados, para lembrar que eles não pertencem ao tempo de previsão.

## 10. Como ler a sobreposição

- Alta pressão + alto índice + ponto ao longo da área colorida: a abertura está lá e é o planejado. Está na hora de chamar nessa direcção.
- Índice alto, mas nenhum ponto: ou ninguém está ativo lá, ou previsão é muito otimista. A camada de escuta ajuda a distinguir: se houver receptores na área e não sentir nada, a segunda hipótese é mais provável.
- Pontos longos fora das áreas coloridas: ducto alto, que as previsões de superfície não vêem, como no evento LA- UA6 [3]; ou outra propagação escapou do filtro (por exemplo, E esporádico sem tag). DF5AI e DK5YA distinguem tropo e E esporádico também da hora: aberturas que nascem e acabam junto com um E difuso esporádico dificilmente são [5] tropo.
- Hepburn e F5LEN discordam: compare as duas fontes com os botões e veja onde os pontos reais caem.
- Pressão baixa: G4MCU seguiu uma abertura no farol SK6VHF até que a pressão caiu; 1012 hPa considerou o evento final [13]. Observe a isobare rodando a barra de tempo: se a alta pressão se retira, a abertura provavelmente segue.
- Sinais que sobem: no estúdio WSPR a 144 MHz do KP4MD a relação sinal/ruído cresceu nas janelas onde Hepburn previu uma melhoria, e caiu quando a previsão levou [14]. se no mesmo caminho cresce, a abertura está se formando.
- Caminhos de montanha: o índice não leva em conta os relevos [1], mas as conexões além dos Alpes também existem graças às células convectivas e ondas de gravidade atmosférica [4], e LA0BY trabalhou 9A e S5 através das montanhas sem Hepburn prevendo-o [3].

## 11. Ponto em aberto

- Permissão de Hepburn e F5LEN para usar seus mapas no programa.
- Calibração dos coeficientes de conversão F5LEN → escala 0–10.
- Calibração de pontos de controle de qualquer tipo de mapa, para ser remake se fontes alterarem gráficos.
- Camada opcional com temperatura à altitude (850 hPa) do GFS.

## Fontes principais

1. W. R. Hepburn, DX Info Centre, “The Hepburn Tropo Index (HTI)” e “Tropospheric Ducting Preview” (dxinfocentre.com). Definição e escala do índice, valores negativos e mais de 10, exclusão de espalhamentos e relevos, passos de 3 e 6 horas em 6 dias, atualização para 18 UTC. Origem dos dados do modelo canadense global: G. Meisse, AMSAT-BB, 27/08/2007.
2. F5LEN, previsão para a Europa (tropo.f5len.org/previews-for-European) e previsão mundial da propagação tropoférica VHF (tropo.f5len.org/WW) e observou “Nova versão da ferramenta de previsão de propagação troposférica VHF”. Índice de unidade N, 40 passos de 3 horas, 4 atualizações por dia, valor e localizador por clique.
3. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Gradiente normal 6.5 °C/km e inversões até 10–15 °C/km, função temperatura e umidade, altas pressões estáveis, evento LA– UA6 visível em alta temperatura e não em conexões Hepburn, LA–9A/ S5 além das montanhas.
4. F. Egano IK3XTV, “Propagação VHF – A contribuição de células convectivas e ondas gravitacionais atmosféricas na propagação Tropo.” Importância dos primeiros 3 km, caminhos marinhos, células convectivas, ondas de gravidade nos Alpes, ligações de alta pressão, volume comum até 3-4 km.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Propagação de Distância Muito Longa na Banda de 144 MHz”, 24/7/2003. Perfis verticais de temperatura e ponto de orvalho para reconhecer inversões, uso do índice de Hepburn, distinção entre tropo e E esporádico de acordo com a hora.
6. J. N. Gannaway G3YGF, “Propagação de Dispersão Troposférica”, QST Novembro 1983. Equivalência do raio terrestre e perda do caminho utilizado para a placa de índice e ponto (ver documento 1).
7. O livro VHF/UHF DX, capítulo 2 “Propagação Troposférica”, pp. 2-23...2-30. Figura de zonas de refração de acordo com dN/dh (padrão −39 N/km, limites −79 N/km e 0) e perfis de dutos de superfície e altos. O texto da cópia fornecida não é legível: apenas as figuras foram utilizadas.
8. Resumo do projeto de 8/11/2026, fornecido pelo autor do programa. Cores do mapa, projeções necessárias, camadas, barra de tempo.
9. Terra natural, dados vetoriais de costas e fronteiras, domínio público (naturalearthdata.com). Referência geral, não anexa.
10. J. P. Snyder, “Projeções do mapa – um manual de trabalho”, USGS Professional Paper 1395, 1987. Formulários de projeções Mercator e azimutale equidtanti. Referência geral, não anexa.
11. Recomendação ITU-R P.453, índice de refração por rádio. Fórmula refractiva N e limiar do canal a −157 N/km. Referência geral, não anexa.
12. NOAA NCEP, Sistema de Previsão Global (GFS). Pressão ao nível do mar a 0,25°, quatro corridas por dia. Referência geral, não anexa.
13. G4MCU, artigos com tag “troposférica” (g4mcu.net/tag/troposférica), 2018-2025. Refração, conduíte e dispersão; dutos mais frequentes com alta pressão, especialmente no mar; farol SK6VHF seguiu até a queda de pressão em 1012 hPa (junho de 2019).
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR Propagation Study” (qsl.net/kp4md/144_mhz_wspr.htm), 2012-2014. O SNR dos relatórios WSPR para 144 MHz cresce nas janelas de melhoria previstas por Hepburn e diminui quando a previsão desaparece.
15. Open-Meteo, API de previsão meteorológica (open-meteo.com), uso não comercial gratuito. Pressão ao nível do mar do modelo GFS em múltiplos pontos em um pedido.
16. mqtt.pskreporter.info, distribuição MQTT em tempo real dos comerciais do pskreporter. informação: nomeação e proprietário dos destinatários (ver documento 1).
