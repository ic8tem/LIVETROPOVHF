LIVETROPOVHF · Documento técnico 1
# Operação do programa

> Tradução automática por italiano: em caso de dúvida vale o guia italiano.

O que o programa faz, de onde leva os dados e como os transforma em o que você vê no mapa e na lista. Versão de 9 de novembro de 2026, relacionada ao projeto em C# (.NET 10 + Avalonia).

## Em resumo

- Objetivo: previsão de tropo (Hepburn [3], F5LEN [4], isobre GFS [18]) e observação real de bandas na mesma janela, de acordo com a lista de controle LA0BY [1].
- Coleta de dados: dois clusters DX, Rede de Beacon Reverso [9], cluster dm4x e fluxo PSKReporter [5] MQTT, decodificação UDP de WSJT-X, JTDX e MSHV [8].
- Spot: segmentos de banda, filtro negativo, airscatter em amarelo tracejado, posição por localizadores ou C ALL3.TXT [6] [7].
- Quem está ouvindo: círculos rosa escuro com linhas rosa sempre visíveis em direção às estações sentiu [5].
- Cartão de ponto: distância, azimut, índice de tropo no caminho e perda de troposcatter com o método G3YGF [2].
- Idiomas Hepburn e F5LEN: [3] imagem e banco de calibração [4].
- Assista: Controle NTP em 10 servidores a cada 3 minutos [10] [11].
- Relatos suspeitos: EME no Q65/JT65 reconhecido com a posição da Lua, localizadores truncados, skimmers, quarentena e ouvintes, com regras aprendidas pelo sol [20].
- Critérios para as devoluções e significado das cores resumidas em dois quadros (secções 11 ter e 11 quater).
- Temas, 36 Línguas [17], exportar documentos para Word com fórmulas editáveis [16], programa portátil e multiplataforma [12] [13] [14] [15].

## 1. Objecto

O programa coloca em uma única janela duas coisas que normalmente olham para sites diferentes: previsão de propagação de troposfer (índice Tropo de Hepburn e F5LEN, isobare) e observação do que realmente acontece em bandas (ponto de DX cluster, Rede Reverse Beacon e PSKReporter, além da lista de estações que estão ouvindo). A faixa de referência é de 144 MHz; os pontos coletam de 50 MHz para cima.

A configuração segue a lista de controle proposta pelo LA0BY para eventos tropo extremos: nos dias anteriores você olha para as previsões Hepburn e F5LEN e os perfis de temperatura em altitude, enquanto o evento está no cluster DX e no local mapeia as evidências de que a abertura está começando [1]. LA0BY também observa que os únicos mapas Hepburn/ F5LEN não são suficientes para prever as ligações mais longas [1]

## 2. A janela e as abas

A janela é uma e não se divide em janelas móveis. De cima para baixo:

- Linha de configurações gerais: Chamada e Localizador à esquerda; à direita o relógio UTC (por exemplo 08/11/2026 20:33:38 UTC, texto âmbar) seguido por NTP:OK ou NTP:FAILED, o menu de tema e o da língua.
- Número de abas em cinco grupos: Mapa e ponto; Previsão (Propria β, a previsão calculada pelo programa e descrita no Manual de Previsão, então Hepburn e F5LEN); GRAVES; Dados e fontes (DX cluster, PSKReporter, Reverse Beacon, UDP, Nomeações e Relatórios suspeitos); Guias (Operação de Programa, Construção de Mapas, Como moldar o tropo, Previsão Manual, Dicas para o operador, com uma caixa de pesquisa que conta em que guia o texto aparece). Sob a viga uma barra mostra as sub-línguas do grupo; cada grupo lembra a última utilizada.
- Conteúdo da aba e, na parte inferior, a barra de status com o estado das fontes, o banco de dados de nomes e a hora da última atualização.

As configurações da linha superior aplicam-se a todas as abas: o tema escolhido também coloriza a coleta de dados e o fundo dos documentos; Páginas do documento permanecem tão brancas quanto uma folha, então eles imprimem bem.

## 3. Chamada e senhorio

O proprietário Maidenhead pode ter 4 ou 6 caracteres (por exemplo, JN70 ou JN70CN). O programa converte-o para as coordenadas do centro do quadrado: campo de 20° × 10°, quadrado de 2° × 1o, subquadrado de 5′ × 2,5′. Se o texto não for um localizador válido, a borda da caixa fica vermelha e a estação não aparece no mapa. O localizador também serve como um centro da projeção do azimute e como um ponto de partida para distâncias e azimute na placa de anúncios.

A Chamada é o nome usado como login de cluster. No primeiro início, até que haja uma nomeação válida, os clusters não se conectam: uma janela pede o nome (e, opcionalmente, o proprietário); confirmado, o nome é copiado como um nome de usuário no campo Login de cada cluster que o tem vazio e clusters sair. Com "Mais tarde" permanecer imóvel, e o botão Connect de cada cluster requer a nomeação novamente. MQTT, UDP e NTP não usam login e sair de qualquer maneira.

## 4. Coleta de dados: fontes

Cada fonte tem sua própria seção na aba Coleta de Dados, com Conectado/Desconectado, as caixas Reconhecidas e Conectadas na inicialização, status, metros e registro das linhas recebidas. As linhas não largadas ou descartadas do filtro não aparecem no registo: apenas contam os contadores de secção. As seções são lado a lado com dois a dois.

| Origem | Ligação | O que traz |
| 1. Escolha do cluster DX | on0dxk.dyndns.org:8000 comando set/filter dxbm/pass 6 | Local de rádios amadores, também SSB e CW. No ON0DXK os VHF são filtrados e estão habilitados com o comando indicado (poi pass 4, 2...). |
| 2. Segunda fonte | dxmaps.com:7300 (DXMAPS, EA6VQ) e s50clx.si:41112 ( S50CLX, Eslovénia) | DXMAPS coleta pontos VHF e também dá pontos de skimmer RBN (set / controle de skimmer, já definido); S50CLX é um cluster DX Spider. Entre com seu nome; endereços editáveis. |
| 3. Rede de Beacon Reverso | Telnet.reversebeacon.net 7000 CW/RTTY · 7001 FT8 | Ponto de skimmer automático. Entre com o nome apenas [9]. |
| 4. Agrupamento com PSKReporter | dm4x.ddns.net:8500 | Pontos digitais do PSKReporter em formato de cluster. |
| 5. PSKReporter MQTT | mqtt.pskreporter.info 1883 · 1884 com TLS | Cada relatório de recepção em tempo real: quem transmite, quem escuta e seus Locadores [5]. Bandas 144, 222 e 432 MHz. |
| 6. Mapas meteorológicos e tropo | dxinfocentre.com/tropo_eur.html tropo.f5len.org/previsions-for-European modelo GFS | Mapas de índice de tropo de Hepburn [3] e F5LEN [4], pressão do nível do mar para isobare (GFS via Open-Meteo [18]). Hepburn e F5LEN devem ser permitidos aos autores. |

Cluster telnet (fontes 1–4). Após a conexão o programa aguarda a solicitação de nome (texto que contém login, chamada:, callsign ou sua chamada); se não chegar dentro de 4 segundos, por favor envie o login. Em seguida, envie os comandos da seção, separados do ponto e vírgula, um a cada 0,6 segundos. Os caracteres de controle telnet são removidos antes de ler as linhas. Se a conexão cair e Riconnetti estiver ativa, tente novamente após 10 segundos (15 se o servidor não responder em tudo) [6].

PSKReporter MQTT (fonte 5). O programa se inscreve no tópico pskr/filter/v2/{banda}/{modo}/{tx_call}/{rx_call}/{tx_grid}/{rx_grid}/{tx_dxcc}/{rx_dxcc} fixando apenas a banda e deixando os outros campos livres com o personagem + [5]. O nome exato com o qual o servidor indica as bandas (por exemplo 2m, 1,25m, 70cm) deve ser verificado na primeira conexão.

## 5. Da linha de cluster para o local

Uma linha é um ponto quando tem a forma DX de SPOTTER: frequência DX comentário hhmmZ. O programa deriva spotter, estação DX, frequência em kHz, comentário e agora UTC; os nomes tiram os sufixos de skimmer (-#, -1...). Em seguida, aplicar, nesta ordem, as seguintes regras [6].

| Banda | Segmento aceito |
| GRAVES (com 144) | 143,040 – 143,060 MHz |
| 144 MHz | 144 – 148 MHz |
| 222 MHz | 220 – 225 MHz (apenas Região 2 ITU, Américas) |
| 432 MHz | 420 – 450 MHz |
| 1296 MHz | 1240 – 1300 MHz |
| 2320 MHz | 2300 – 2450 MHz |
| 10 GHz | 10,5 GHz |

1. Banda. A frequência deve cair em um dos segmentos gerenciados; todo o resto, e em particular todos os comerciais abaixo de 49 990,0 kHz, não é mostrado e conta como “fora da banda”.
2. Muito. Uma das formas mais conhecidas de comentar é FT8, FT4, Q65, JT65, ISCAT, CW, SSB, FM, RTTY, PSK31, MSK144, FSK441, JT6M); USB e LSB tornam-se SSB.
3. Filtro negativo. O local é descartado se contiver os modos MSK144, FSK441, JT6M, JTMS, ISCAT ou as tags <AUE> <ES> <MS> <FAI> < F2 >, com maiúsculas não sensíveis e mesmo espaços entre parênteses. Estas são formas e propagações que não são tropo: Meteorito disperso, aurora e esporádico, F2.
4. Airscatter. Se o comentário contiver <AS> ou <ACS> (também misturado, por exemplo, <As>) ou declarar o local como airscatter (airscatter, scatter de ar, scatter de avião), o ponto permanece mas é desenhado em amarelo com linha pontilhada (ondulado escuro para permanecer legível no mapa claro) e destacado na lista.
5. Localização das estações. Se no comentário há dois localizadores (por exemplo, JN54 < JN83) o primeiro é daqueles que fazem o local e o segundo da estação DX; com um localizador, que é da estação DX. O que está faltando é pesquisado no C ALL3. TXT, apenas com o nome exato (para laptops veja abaixo). Se uma estação permanecer sem uma posição, o local aparece na lista com a nota “posição desconhecida” mas não no mapa.
6. Duplas. Um comercial com o mesmo DX, mesmo observador, mesma banda e mesma fonte de um já presente substitui-o, em vez de adicionar.

C ALL3.TXT é o banco de dados de nomes → localizador usado pelo JTDX, no formato CALL,LOCATORE,,. O arquivo do projeto contém 1 048 847 indicações, todas com localizador válido; o cabeçalho do arquivo ainda fala de 34 323 nomes porque permaneceu o da primeira edição [7]. Ele é carregado no fundo na inicialização, então a janela é imediatamente utilizável. O arquivo é pesquisado ao lado do programa, na pasta Dados, na pasta a partir da qual o programa foi iniciado e uma ou duas pastas acima; Se não houver, uma janela lhe dirá quando você iniciar e a barra de estado o sinalizará em vermelho. Seu nome sempre usa seu 6 caracteres. Apenas regra para os localizadores: se o recebido (ponto, MQTT, decodificação, QSO desunti) tem menos de 6 caracteres e C ALL3. TXT tem um a 6, sempre vale a pena o C ALL3.TXT; para laptops (F/ IC8TEM, IC8TEM/P) é apenas uma voz exata: sem, a estação permanece sem um proprietário e não é projetado em outro lugar. A regra também se aplica no momento do desenho, de modo que os dados chegados antes do banco de dados foi carregado será executado em si.

Uma base de dados: C ALL3.TXT. A partir de 1.3.4 todos os localizadores estão em um arquivo, C ALL3.TXT, que permanece no formato de JTDX e WSJT-X (CALL,LOCATORE,,,,) e pode ser usado como também em WSJT-X, JTDX e MSHV. Não há mais arquivos separados. No topo do arquivo, após o cabeçalho, há dois blocos marcados por uma linha de comentários: // [LIVETROPOVHF-MANUALI] com as entradas inseridas à mão e // [LIVETROPOVHF-CLUSTER] com as correções do cluster; então // [LIVETROPOVHF-FONTI] com todos os outros itens em ordem alfabética. Cada nomeação aparece uma vez. Na sub-língua Dados e Fontes › Nomeações (C ALL3) você procura um nome, você escreve o proprietário e pressiona Adicionar / mudar: a voz vai para o bloco de vozes de mão e vale acima de tudo, mesmo acima do localizador que a própria estação envia. ∫ Remove a nomeação do C ALL3.TXT (a próxima atualização é retomada a partir de fontes, se elas tiverem). Adicionar um arquivo... leia uma lista CALL, LOCATOR (você também aceita o ponto e vírgula, tabulação e espaço) e escreva-o entre vozes escolhidas manualmente; o arquivo escolhido não é copiado. Reler reler C ALL3.TXT do disco, depois de editá-lo com um editor. Ordem completa das fontes: localizador próprio, vozes de mão, correções de cluster, o restante de C ALL3. TXT (fontes, localizadores ao vivo aprendidos e G7VRD), recebeu localizador (que se aplica sobre a base de dados apenas se tem 6 caracteres e não termina com LL ou MM).

Atualização de C ALL3.TXT. Na mesma sub-língua o botão Atualizar agora baixa as fontes (C ALL3.TXT do WSJT-X Melhorado, C ALL3.TXT EME do W7GJ, grid_data.bin do JTDX), junta-se a eles (o primeiro que tem uma vitória de nomeação), verifique se eles são pelo menos 50.000 nomes (caso contrário nada muda), salva o C ALL3. TXT O anterior C ALL3. As entradas TXT permanecem, e um localizador de 6 caracteres permanece onde uma fonte tem apenas 4 do mesmo quadrado. Vozes de mão e correções do cluster permanecem no topo, inalterados. Com a caixa Atualizar por conta própria todos os dias, a verificação ocorre no início. O programa reescreve C ALL3. TXT imediatamente após cada voz à mão e, para os localizadores aprendidos, no máximo a cada 10 minutos e no fechamento; o C ALL3. TXT do pacote é somente leitura e o atributo se remove. Os instaladores Linux não sobrepõem um C ALL3. TXT já está presente; que para o Windows só o instala se estiver faltando. ALL3

Correcções automáticas do cluster. Nos pontos automáticos DM4X e RBN o comentário traz dois localizadores, por exemplo KN09tp<> KN09: o primeiro é o que a estação que os pontos se declara, para 6 caracteres; o segundo é o da estação sentida, muitas vezes apenas para 4. Se o primeiro tem 6 caracteres (e não termina com LL ou MM) e é diferente do da base de dados, o programa corrige C ALL3. TXT sozinho: a correção vai no bloco // [LIVETROPOVHF-CLUSTER], com a data da última confirmação no terceiro campo, é válida acima das fontes (sob as vozes da mão) e aparece entre os sinais suspeitos atualizados como "C ALL3 ". Então ele sobrevive C ALL3. Atualizações TXT. Uma correção não confirmada por dois anos perde precedência e permanece como uma voz normal.

Última fonte: G7VRD. Se um candidato não tiver 6 caracteres no C ALL3. TXT, o programa pede em segundo plano para a API pública do G7VRD (api.g7vrd.co.uk, “localizador por callsign”), que coleta localizadores automaticamente do tráfego de rede. Você só aceita um 6-caracteres que não é o centro do quadrado (LL ou MM); as solicitações são agrupadas, no máximo 40 nominativos por minuto, e cada nominativo pede uma vez por dia, porque é um serviço hobbistic sem garantias. O localizador encontrado entra em C ALL3. TXT se o nome não estiver lá ou tiver apenas 4 caracteres do mesmo quadrado; a pesquisa desliga com a caixa no nome sublingual.

Nomes do bar. Nos tópicos do PSKReporter MQTT, a barra separa os níveis, depois os nomes compostos vêm com um ponto em vez da barra ( IV3. HB9CAT). O programa coloca a barra de volta e, se a mensagem contém os nomes completos, use aqueles: IV3/HB9CAT permanece IV3/HB9CAT no mapa, lista e banco de dados.

Os localizadores aprenderam ao vivo. C ALL3. TXT não contém tudo: por exemplo SP9EMF e SP9NVY não estão lá, e então permanece o localizador de 4 caracteres enviado pelo PSKReporter (o cartão diz isso). Para reduzir esses casos, o programa escreve em C ALL3. TXT a cada 6 caracteres que vem de qualquer fonte (PSKReporter, como transmissor ou como receptor, cluster, decodificação UDP) para uma indicação que C ALL3. TXT não tem ou tem apenas 4 caracteres no mesmo quadrado; nunca muda uma voz para 6 caracteres. Até 1, 3 estes localizadores estavam em Data/ LOC6. TXT.

Transição de 1.3.3. No primeiro início de 1.3.4 os arquivos além das versões anteriores (Dati/C ALL3 -MANUALE.TXT, o Data/C ALL3 -AGGIUNTE, Data/C ALL3 -CLUSTER. TXT, Data/ LOC6.TXT e Data/C ALL3 - G7VRD.TXT) são fundidos em C ALL3. TXT com os mesmos itens. Libertando a escrita, os arquivos antigos pegam a extensão.fuso, você não lê mais e você pode excluir.

Receptores automáticos. RBN, dm4x e PSKReporter frequentemente reportam nomes com um sufixo indicando o receptor: -SDR, /SDR, -# (skimmer RBN) ou -1, -2... (mais receptores da mesma estação). Para o proprietário o sufixo é removido: OK2OZL -SDR leva OK2OZL JN89UE de C ALL3.TXT se a estação não enviar um localizador de 6 caracteres. O nome mostrado permanece aquele com o sufixo.

Os localizadores cortam. Nem todas as fontes curam a extensão de seu senhorio: muitos a configuram para 4 caracteres, e alguns sistemas de reporte automáticos truncam deliberadamente o localizador de 4 caracteres, mesmo aqueles que obtêm os dados da escuta real de FT8, FT4, Q65 e JT65, ou completam-no com o centro do quadrado (LL ou MM, por exemplo JN89MM). Por essa razão, um 4 caracteres, ou 6 caracteres que termina com LL ou MM, é substituído pelo C ALL3. TXT do mesmo quadrado. Se o local correto e o centro da praça não coincidir com algumas dezenas de km, o erro permanece pequeno em comparação com as distâncias tropo.

## 6. Quem está a ouvir?

Cada mensagem MQTT do PSKReporter relata o nome e o localizador daqueles que recebem [5], então você não precisa procurá-lo em C ALL3.TXT. Para a banda escolhida no seletor o programa detém, para cada estação receptora: proprietário, última recepção, formas decodificadas, número de estações sentidas e distância máxima. Uma estação deixa a lista quando não recebe mais nada na faixa escolhida (3, 5, 10, 15, 30 ou 60 minutos).

No mapa cada receptor é um círculo vazio rosa escuro, e de cada início linhas rosa completas para as estações que estão sentindo e envia de volta para a rede. As linhas são sempre vistas, sem clicar. As nomeações das estações de escuta, os ouvidos e as duas cabeças de cada ponto são sempre escritas no mapa, sem ter que clicar (o mesmo nome no mesmo ponto é escrito apenas uma vez). Ao clicar em um círculo, suas linhas se tornam ainda mais espessas e abre a aba com detalhes. Se o localizador do MQTT tem menos de 6 caracteres, o programa usa o de C ALL3.TXT (cerca de um milhão de nomes), desde que seja consistente com o quadrado recebido.

Meteorito de dispersão. Assim como em pontos de cluster, as razões MQTT em MSK144, FSK441, JT6M, JTMS ou ISCAT são descartadas e contadas apenas; o mesmo vale para as decodificações de UDP dessas formas.

Não são verdadeiros ouvintes. Algumas estações declaram receber em 144 MHz, mas na realidade o receptor está em outra faixa (indicação de frequência errada): seus relatórios projetariam aberturas inexistentes de milhares de km. O programa verifica-o sozinho. Cada estação que um ouvinte diz para ouvir em VHF é seguida por 30 minutos em todas as bandas, com uma inscrição MQTT dedicada (pskr/filter/v2/+/{nominative}/#) [5]. servir como prova. Se pelo menos duas das estações ouvidas por um ouvinte são, nos mesmos minutos (±10), ativas em outras bandas, o ouvinte é considerado não genuíno e todos os seus relatórios VHF são descartados; o registro MQTT relata o nome e as estações que o traíram. Na lista de pontos, ao lado dos medidores, o botão MQTT mostra o estado do fluxo (folha verde se conectada, vermelha se não) e quantos ouvintes foram descartados na faixa e intervalo escolhido; clicando nele abre a aba com a lista de devoluções e, para cada uma, estações encontradas em outras bandas.

China de ligação longa. Um link sobre 1000 km (Clumbing Spot, MQTT Listening, QSO Desunti) não é mostrado imediatamente: permanece detido, e o contador “held > 1000 km” diz o número. É revelado quando outra conexão, entre diferentes estações e pelo menos metade ao longo, confirma-o com um destes critérios: mesma estação em uma cabeça (por exemplo, o mesmo ouvinte) e a outra estação na mesma direção dentro de 10°; corredor semelhante, com ambas as cabeças dentro de 200 km de outra conexão; rota paralela, com direção dentro de 10° e distância lateral dentro de 200 km, sobrepôs-se ao longo da rota. Ambos aparecem e o registo do MQTT mostra a razão. Suas decodificações UDP são sempre visíveis e se aplicam como confirmação para outros. As conexões retidas permanecem na memória por 60 minutos.

Nada abaixo de 144 MHz. O programa funciona de 144 MHz para cima: tudo abaixo de 143 MHz é descartado. A exceção é a GRAVES, o radar francês que transmite em CW em 143.050 MHz por Broye-lès-Pesmes ( JN27SI): um comercial entre 143.040 e 143.060 MHz, ou com GRAVES no nome, aparece entre 144 MHz com radar como transmissor. A placa de anúncio diz se o receptor está dentro do feixe (setores D, C, B, A de 90° a 270° de azimut visto do radar, 6 passos de 7,5° cada) ou para fora; a aba GRANVES, como em YA-MS, mostra o céu visto do radar: setores, passo iluminado naquele momento (sequenza PE1ITR 2021: 0,8 s por passo, ciclo 4,8 s Os dados e geometria vêm do YA-MS. Sentir isso no trope é difícil, mas pode acontecer.

Reabilitação de grupos humanos. Uma estação que também aparece em outras bandas não conta como suspeita se, nos mesmos minutos (±30), os clusters humanos DX ( ON0DXK, GB7RAU; não dm4x, RBN ou outras fontes automáticas) os danos ativos na banda em questão, em pontos feitos por outros nomes: é o caso de estações que realmente trabalham em múltiplas bandas juntas. Assim, um ouvinte descartado retorna visível (o registro MQTT escreve “reabilitado”) e um link de longa data para bandas baixas pode ser revelado.

Estações activas abaixo de 144 MHz. Na quarentena de conexões longas, uma estação que nos mesmos minutos é ouvida em faixas abaixo de 144 MHz (a partir dos relatórios MQTT das outras faixas) não confirma outras conexões e não é revelado, embora uma rota semelhante ou paralela iria confirmá-lo.

## 7. Lista de anúncios comerciais

Ocupa 35% à direita da guia Mapa e local. Topo: título, seletor de banda (50, 70, 144, 222, 432, 1296, 2320 MHz, 10 GHz), seletor de faixa (últimos 3, 5, 10, 15, 30, 60 minutos) e chave de atualização. Abaixo, quatro metros: pontos visíveis, AS/ACS, descartados do filtro, estações de escuta.

Cada linha mostra o tipo de linha, o tempo UTC, DX → DE, o caminho, a distância em km e, na segunda linha, a fonte, frequência e comentário. O mais recente está no topo. Um clique na linha seleciona o local: no mapa a linha cresce e os dois nomes aparecem.

## 8. Perda de cartão spot e troposcatter

Para o local selecionado a aba mostra localizadores, distância, azimut, frequência, modo, fonte, agora, comentário, o índice de tropo médio ao longo do caminho e uma estimativa da perda de rota para tropo disperso.

Distância e azimut são calculados no círculo máximo com raio da Terra de 6371 km (formula do emisenoverso para distância, azimut inicial da estação DX para o observador).

Perda de troposcatter. A perda básica, para duas estações em terreno liso, é a soma de perda de espaço livre e perda de disseminação: G3YGF [2]

```math
L_{fs} = 32,5 + 20 \log D + 20 \log f
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
\theta_{s} = \theta_{A} + \theta_{B} + 57,3 \cdot \frac{D}{8497}
L = 53,5 + 20 \log D + 30 \log f + 0,0674 \cdot D + 10 (\theta_{A} + \theta_{B})
```

As perdas são em dB, D é a distância em km, f a frequência em MHz, Δs o ângulo de difusão em graus.

8497 km é o raio equivalente da Terra (4/3 do físico). O horizonte de cada estação é determinado por:

```math
\theta = 57,3 \cdot \left( \frac{\Delta h}{d} - \frac{d}{2R} \right)
```

com Δh altura do obstáculo que faz a partir do horizonte em relação à estação e sua distância. Cada nível de horizonte custa cerca de 10 dB [2]. O programa não conhece o horizonte das estações, por isso mostra a perda básica com ωA = ωB = 0 e declara-o ("terreno suave"). G3YGF adiciona um termo climático, onde N é a reatividade ao solo [2]; será usado quando N está disponível a partir do modelo meteorológico:

```math
L_{N} = 0,2 \cdot (N - 310)
```

Verificação: para 110 km a 144 MHz a fórmula dá 166,5 dB, contra cerca de 168 dB leitos no gráfico do artigo. Para 500 km a 144 MHz dá cerca de 206 dB. A relação sinal/ruído esperado é a capacidade do sistema (eirp + sensibilidade efetiva do receptor) menos a perda: com o sistema G3YGF 144 MHz modelo (198 dB) um caminho de 500 km daria cerca de −8 dB em SSB [2], ou seja, a conexão é apenas com melhor propagação da dispersão pura.

## 9. Minhas decodificações: UDP de WSJT-X, JTDX e MSHV

WSJT-X, JTDX e MSHV podem enviar UDP cada linha que decodificam. O programa escuta 127.0.0.1:2237 e repete o mesmo pacote, byte por byte, em 127.0.0.:2238, então os outros programas que usaram essa porta (GridTracker, JTALert, um registrador) continuam a funcionar se você movê-los em 2238. Endereços e portas mudam no UDP sublingual da Coleção de Dados; como um endereço de escuta você também pode usar um grupo multicast (por exemplo, 224.0.0.1), se o programa de decodificação é definido como este.

Os pacotes seguem o protocolo de rede WSJT-X: um número mágico inicial 0xADBCCBDA, número de esquema e tipo de mensagem. O programa utiliza principalmente três tipos [8]:

- Heartbeat: diz qual programa está transmitindo os dados e com qual versão.
- Estado: frequência do mostrador e modo; a partir da frequência você obtém a banda (com a mesma tabela dos pontos).
- Descodificação: Agora, SNR, tempo e frequência de desperdício, forma e texto da mensagem decodificada.

A partir do texto você recebe a estação que você ouviu: no CQ 9 A2PQR JN83 é a estação após CQ (salvando DX, UE, TEST...), em IK0JKL IT9MNO JM68 é a segunda, ou seja, que transmite. O localizador é o da mensagem, se houver (4 caracteres, diferente de RR73); caso contrário você procurar em C ALL3. TXT. Eles estão ouvindo linhas: estações que seu receptor ouve agora. No mapa eles se tornam linhas vermelhas do seu QTH para a estação, com o nome; a camada se ilumina com a caixa Decodificada de mim e requer um localizador válido. Na lista à direita, um contador diz quantas estações você está decodificando na banda escolhida.

QSO desunti dal ao vivo. Quando uma decodificação é uma mensagem de fechamento no formulário C ALL1 C ALL2 RR73 (ou 73 ou RRR), por exemplo IC8TEM IK3FHP RR73 ou IK3FKP SV8PEX 73, as duas estações completaram um link. O programa registra-o como QSO, com o localizador de cada um retirado da última mensagem em que o declarou ou, se não, de C ALL3.TXT [7], e desenha-o no mapa como uma linha verde entre as duas estações (estratura “QSO desunti dal live”). Um par de estações contam uma vez por banda; a banda e o intervalo escolhidos na lista são válidos.

Na mesma sub-língua, o painel Debug mostra o texto de cada pacote recebido (tipo, tempo, valores e mensagem), a pedido também bytes hexadecimais, e uma linha para encaminhamento. É para verificar que o programa de decodificação está realmente enviando os dados.

## 9 bis. Línguas Hepburn e F5LEN

Cada mostra o que o programa tira da fonte. O botão “Leia a página e baixe todas as imagens” abre a página indicada (base dxinfocentre.com/tropo_eur.html para Hepburn [3] e tropo.f5len.org/forecasts-for-europe para F5LEN [4]) e baixa todas as imagens no cache ao lado do executável; você também pode colar o endereço de uma imagem. Para cada imagem existe uma caixa para excluí-la e um campo com o tempo de previsão. As 31 imagens europeias do F5LEN são nomeadas pelo tempo da corrida modelo (03.png... 48.png a cada 3 horas, 54... 96 a cada 6 horas, 108... 192 a cada 12 horas) e o programa leva o tempo do nome do arquivo [4]; para Hepburn vale a ordem da página. Na página F5LEN, um menu escolhe a região: Europa (tropo.f5len.org/precasts-for-europe), Austrália (tropo.f5len.org/Australia, vk03.png... vk192.png) ou Atlântico entre Cabo Verde e Irlanda (tropo.f5len.org/atlantic-ocean/atlantic-d4-ei, atl03.png... atl192.png). Todos têm os mesmos passos.

Abaixo está o contador de calibração. Um clique na imagem adiciona um ponto de controle (localizador ou lat,lon: pelo menos 6) ou uma cor da legenda com seu valor. O programa calcula a correspondência de pixels → coordenadas com uma interpolação polinomial de segundo grau nos menores quadrados [19], lê as cores com a tolerância escolhida e, para F5LEN, converte as unidades N na escala 0-10 com a · N + b. "Salvar calibração e tentar ler" escreve o arquivo na pasta Tarature e verificar qual pixel seu localizador cai. Antes de configurar uma calibração, deve ser solicitada permissão ao autor dos mapas.

Regiões. Cada fonte tem mais regiões, cada uma com suas imagens de nome fixo: Desculpa, desculpa. A página de cada fonte mostra, em primeiro lugar, a antevisão da fusão: todas as imagens transferidas reunidas como no mapa principal, com o painel pontilhado e o nome de cada região, um véu cinzento onde nenhuma imagem cobre a área e uma barra deslizante para rolar as horas; à esquerda a lista de regiões, cada uma com a caixa para usá-la, o número de mapas lidos e de onde vêm os extremos (OCR, à mão ou à fábrica; se o OCR falhar o texto que leu aparece). O banco de calibração manual permanece atrás do botão homônimo. O menu Região de Bench escolhe qual olhar, a caixa “Use esta região no mapa” inclui-o no índice. O quadro é encontrado sozinho (duas linhas horizontais e duas linhas verticais da mesma cor); para colocar a imagem servir os extremos lat/lon do quadro: para Hepburn Europa (10° W-40° E, 27°-56° N) e F5LEN Europa (15° W-40° E, 30°-65° N) já estão no programa, para as outras regiões que escrevem nos quatro campos O E S N lendo os rótulos do eixo. O multiplicador de escala torna as duas fontes comparáveis: valor final = valor multiplicador × legenda. [3] F5LEN [4] F5LEN

## 9 ter. Exportar para a Palavra

Os dois documentos têm no topo do botão “Exportar para o Word”. O arquivo é escrito no formato Office Open XML; as fórmulas tornam-se equações do Word (OMML), para que permaneçam editáveis com o editor de equações [16]. Se a janela de salvamento não estiver disponível, o arquivo termina na pasta Documentos ao lado do executável.

## 9 quater. Quem me chama?

Se uma mensagem for enviada para você no UDP local (o primeiro nome é seu, por exemplo IC8FAX IK3FHP JN55 ou IC8FAX IK3FHP -12), o programa rastreia do seu QTH para a estação que chama você de linha pontilhada e piscando preta, com o nome e o localizador ao seu lado. O localizador vem de C ALL3.TXT (incluindo vozes manuais) ou mensagem. A linha permanece até que o UDP passe o fechamento do QSO com aquela estação ( RR73, RRR ou 73, em uma das duas direções), no máximo 3 minutos da última chamada; você desenha sobre tudo e não segue critérios de exclusão, então uma chamada direta nunca escapa.

O PSKReporter, via MQTT, não transmite o texto das mensagens: diz apenas quem recebeu quem, com frequência, caminho e SNR. É por isso que uma chamada direta só é reconhecida por suas próprias decodificações UDP.

## 10. Controlo do tempo (NTP)

Modos digitais e tempo local exigem um relógio preciso. A cada 3 minutos, o programa interroga 10 servidores NTP (SNTP on UDP 123): 0.pool.ntp.org, 1.pool.ntp.org, 2.pool.ntp.org, 3.pool.ntp.org, it.pool.ntp.org, ntp1.inrim.it, ntp2.inrim.it, ptbtime1.ptb.de, time.google.com, time.cloudflare.com [11].

Para cada resposta calcula-se o descarte e o atraso do retorno, descarta-se os valores anormais e leva-se a mediana, com o cálculo do descarte e atraso fornecido pelo SNTP [10]. Com pelo menos 3 respostas concordam que o relógio UTC do programa se alinha e em seguida aparece NTP:OK em verde; se as respostas não são suficientes aparecem NTP:FAILED em vermelho e o relógio permanece o do computador. O relógio do sistema não é tocado, porque as permissões do administrador seriam necessárias; Todos os horários internos do programa (pontos, janelas de 5 a 60 minutos, decodificações) usam o tempo alinhado. O controle sempre gira em segundo plano: na interface você vê apenas o resultado ao lado do relógio; servidores, intervalos e mudanças de respostas mínimas em LIVETROPOVHF.ini (ntp_servers, ntp_minutes, ntp_min).

## 10 bis. Critérios gerais de exclusão

Duas caixas na barra de camadas aplicam-se a tudo o que desenhar (ponto, PSKReporter, decodificação, QSO desunti):

- Excluir menos de 100 km. O tráfego entre estações inferiores a 100 km pode não ser significativo para o tropo (onda de terra, faixa óptica): com a caixa na qual você não desenha e não aparece na lista. O início está cancelado.
- Mais de 1000 km: apenas quadrados. Ao redor de seu QTH (o localizador definido para começar) há uma coroa de 1000 km em todas as direções. Se apenas uma das duas estações de um link está fora desta coroa, a conexão não é desenhada como uma linha: em seu lugar eles coram em lilás, pontilhados a 45°, os quadrados localizadores (4 caracteres) das estações envolvidas. O mesmo se aplica às estações PSKReporter que ouvem mais de 1000 km, que aparecem como um quadrado em vez de um círculo. Até agora há tráfego sem preencher o mapa de linhas. Sem um localizador válido conta o comprimento da conexão. O quadrado de seu QTH nunca colore em lilás, mesmo quando você ouve uma estação além da coroa: é o localizador especificado, qualquer nome usa quem opera da estação. O raio da coroa é escolhido ao lado da caixa: 1000, 700 ou 500 km. A caixa Corone 1000/700/500 km desenha as três coroas ao redor do QTH, desenhada em lilás, com a escolha mais marcada e a distância escrita norte. Na lista de pontos a distância é seguida por.. O começo começou. O critério aplica-se após a quarentena: as ligações longas ainda retidas nem sequer aparecem como quadrados.

Quais são os quadrados lilás cobertos a 45°? O tráfego longe de seu QTH lhe interessa entender onde a propagação está aberta, mas suas linhas, centenas de quilômetros de comprimento e muitas vezes dezenas, cobrir o mapa e esconder o que conta para você: as aberturas que você pode explorar de sua estação. Por esta razão, além da coroa escolhida, o programa não desenha as linhas, mas coloriza os quadrados localizadores (2° × 1°) das estações envolvidas. O resultado é um mapa de «zonas activas»: onde aparecem muitos quadrados lilás próximos, há um canal ou uma abertura, mesmo que não lhe diga directamente respeito; Se a área se alarga em sua direção, é o sinal para apontar a antena nessa direcção. O painel de 45° permite que você veja sob o índice tropo e a grade, então compare de relance a previsão (cor inferior) e o tráfego real (illus). Seu quadrado QTH nunca colore, porque é o ponto de partida e não uma área distante. Apenas os quadrados que estão todos além da coroa são coloridos: um quadrado atravessado pela coroa, ou por dentro, permanece livre, mesmo que haja uma das duas estações. Para ver as linhas, basta desligar a caixa ou escolher uma coroa maior.

## 11. Temas e línguas

Temas: os 18 programas comuns ao IC8TEM, o mesmo que YA-NEC e E-LAYER ANALYZER: Clear, Night, Black, oito pastels (azul, laranja, aquamarine, verde, rosa, lilás, índigo, marrom), Scia meteórica (Paleta YA-MS), os três de IP-TRX-CTRL (Chiaro, Notte, Nero) e Azul, Roxo, Verde. A partir de 1.3.4 o Chiaro parece YA-NEC: fundo cinza claro, painéis brancos bordados, guias de noite azul e sotaque azul. Em temas escuros também o mapa base torna-se escuro. A escala de cores da previsão permanece a mesma em todos os temas. Em telas estreitas (menos de 1500 pixels, por exemplo 1366×768) o relógio no topo mostra apenas a hora, então o tema, cursor de textos e linguagem permanecem visíveis. Línguas: o 28o wiki de YA-MS [17] (Inglês, Viagens, Deutsch, Nederlands, Français, Español, Port本ês, Dansk, Norsk, Svenska, Íslenska, Eesti, Lietuvi, Latviešu, Руский,,кра,нська, Türkaلبيا Com espelhos de janela árabe, hebraico e urdu inteiro da direita para a esquerda; o mapa permanece como [12]. As traduções dizem respeito à interface; os dois documentos técnicos e mensagens da barra de status permanecem em italiano. Os nomes dos dias vêm das configurações internacionais de. NET para a língua escolhida.

Tema e linguagem mudam dos dois menus no canto superior direito, ao lado do relógio, e se aplicam imediatamente, sem reiniciar o programa e sem interromper a coleta de dados. A escolha é salva em LIVETROPOVHF.ini.

## 11 bis. Relatórios suspeitos e auto-aprendizagem

A aba Relatórios suspeitos coleta tudo o que os controles do programa acham estranho, agrupados por tipo e nome, com as últimas 24 horas listadas. O programa aprende por si só: para cada casal (tipo, nome) conta os resultados positivos (+) e contrário (−), e depois de 3 resultados acordados cria uma regra automática; um resultado oposto coloca em questão. As decisões do usuário (Bom, Pobre) sempre se aplicam; o botão pulsante retorna a decisão ao programa. As regras estão em Data/APPRENDINO. TXT, ao lado do programa; os automáticos não reforçados por um ano são esquecidos.

| Tipo | Quando | Sair + / − | Efeito da Regra |
| EME ( Q65/JT65) | Q65 ou JT65 mais de 1000 km com a Lua acima do horizonte para ambas as cabeças, ou EME em comentário | − cada vez que a Lua é comum | Eliminar: Estação EME, seus Q65/JT65 s longos também são excluídos sem lua comum. Bom: tratado como tropo |
| Ligação longa | Mais de 1000 km, em quarentena | + revelado, − expirado sem confirmação | Bom: os comprimentos da estação mostram imediatamente. Descartar: nunca |
| Ouvinte | receptor PSKReporter que ouve estações activas noutras bandas | − eliminado, + reabilitado por aglomerados humanos | Bom: nunca descartado. Eliminar: sempre |
| Diferente proprietário | Quadrado recebido diferente do de C ALL3. TXT | + cada vez que você repetir | Bom: a estação se moveu, vale a pena o quadrado recebido. Eliminar: vale C ALL3.TXT |
| Centro Quadrado | 6 caracteres com LL ou MM |  —  | Apenas informação |
| Receptor automático | Nomeação com -SDR, /SDR, -# ou -n |  —  | Apenas informação |

EME. Q65 e JT65 são as formas de comunicação via Luna: Em teoria, eles caem na cláusula de exclusão como o meteoro de dispersão, mas eles também são usados em tropo. O programa calcula a posição da Lua com as fórmulas de baixa precisão do Astronómico Almanaque (cerca de 0,3°) [20]: se uma ligação em Q65 ou JT65 exceder 1000 km e a Lua estiver acima do horizonte para ambas as cabeças, é quase certamente EME e está excluída (conta entre os excluídos). Se a Lua não é comum, a conexão permanece possível no trope e passa através da quarentena de conexões longas, mas o sinal permanece visível. Aplica-se aos clusters, MQTT e suas decodificações UDP.

## 11 ter. Critérios de devolução de contacto, um por um

O programa quer mostrar apenas a propagação tropoférica em 144 MHz e além. Cada dado recebido (Cluster Row, PSKReporter Report via MQTT, decodificação UDP) passa por uma série de filtros, sempre na mesma ordem. Um dado descartado não desaparece em nada: é contado (contado dos excluídos na barra superior) e, quando a razão é duvidosa, aparece na aba Relatórios Suspensos.

|  N.  | Critérios | Onde está? | O Que Descartou | Podes mudar-te? |
|  1  | Banda | cluster, MQTT, UDP | Todos abaixo de 143 MHz (também 50 e 70 MHz), exceto GRAVES 143.050 MHz. As bandas realizadas são 144, 222, 432, 1296, 2320 MHz e 10 GHz | Não. |
|  2  | QO-100 Satélite | cluster, MQTT, UDP | 2400,0–2400,5 MHz e 10489,5–10490 MHz: é o tráfego geoestacionário por satélite, não tropo | Não. |
|  3  | Dispersão de meteoros | cluster, MQTT, UDP | JT6M e ISCAT (também ISCAT-A/B), comentários com etiquetas de propagação "&" e não-tropoxi (<AU>, <AUE>, <ES>, <FAI>, < F2 >) sempre. MSK144, FSK441 e JTMS (também com <MS>) apenas mais de 250 km: dentro de 250 km aceitamos, porque a essa distância esses modos também são usados em tropo | Não. |
|  4  | EME (via Luna) | cluster, MQTT, UDP | EME escrito no comentário; Q65 ou JT65 mais de 1000 km quando a Lua está acima do horizonte para ambas as cabeças; estações aprendidas como EME | Sim: Boa regra em sinais suspeitos |
|  5  | Ouvinte não genuíno | MQ | receptor PSKReporter que diz para ouvir em 144 estações ativadas naquele momento em outras bandas (pelo menos duas): As relações dele são descartadas. Voltar bom se os grupos humanos DX confirmar as mesmas estações | Sim: Bom / Pobre |
|  6  | Quarentena de ligações longas | todas as linhas | Mais de 1000 km uma ligação permanece escondida até encontrar uma confirmação: mesma estação na mesma direcção, outra rota num corredor de 200 km, ou rota paralela dentro de 10° e 200 km ao longo de pelo menos metade. Sem confirmação expira após 60 minutos | Sim: Bom / Off para a estação |
|  7  | Menos de 100 km | linhas e lista | Com a caixa de acesso, as conexões mais curtas de 100 km (onda de terra, alcance óptico) | Caixa, desligada na inicialização |
|  8  | Além da coroa do QTH | linhas | Se uma estação é mais de 1000, 700 ou 500 km (à sua escolha) de seu QTH, nenhuma linha: apenas os quadrados de lilla cobertos pelas duas estações. Seu Quadrado QTH Nunca Cores | Caixa e raio, em 1000 km |
|  9  | Janela de tempo | tudo | Pontos mais antigos do intervalo escolhido (de 3 a 60 minutos) | Menu no topo |
|  10  | Duplo | cluster | O mesmo local (mesmo DX, mesmo observador, mesma banda, mesma fonte) repetido | Não. |

Nota sobre os critérios 4 e 6. Uma ligação em Q65 ou JT65 superior a 1000 km sem Lua Comum é possível em tropo: não é descartada pelo critério 4, mas passa para a quarentena do critério 6 como qualquer outra ligação longa. Chamadas diretas para você (seção 9 quater) são a única exceção aos critérios 7 e 8: eles sempre desenham.

O que não é um desperdício. Correcções do localizador ( C ALL3. TXT com vozes manuais, correções do cluster e localizadores aprendidos, sufixos -SDR, centro do quadrado LL/MM) mudar onde você desenhar uma estação, não descartá-lo. Uma estação sem proprietário, por outro lado, não pode ser desenhada: permanece na lista com a distância «—».

## 11 quater. Cores e símbolos

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

## 12. Configurações e portabilidade

Janela. No início, a janela ocupa 90% do espaço útil da tela (cortar a barra de aplicação), centrado, então em um notebook de 15-16 polegadas com 125% ou 150% redimensionar a barra de título permanece visível. Se o espaço útil estiver abaixo de 1280 × 720 pontos, a parte da janela maximizada. Tamanho, posição e status maximizado são salvos em LIVETROPOVHF. ini no fechamento e fim no próximo início, desde que a barra de título ainda caia em uma tela (por exemplo, após desacoplar um monitor externo); caso contrário retornaremos a 90%. O tamanho mínimo é de 900 × 560.

O programa é portátil: nunca escreve na pasta AppData ou no registro. Tudo está na pasta do executável: LIVETROPOVHF.ini (sets, criado no primeiro fechamento), C ALL3.TXT (com vozes escritas à mão, correções aprendidas e localizadores), cache de mapas e dados meteorológicos, regras aprendidas (Dati/APPRENDINO.TXT) e arquivo LIVETROPOVHF.db. Você também pode mantê-lo em um pendrive USB, desde que a pasta é escrita.

Arquivo de anúncios (SQLite [14]). Cada ponto aceito e cada relatório de escuta PSKReporter são salvos em LIVETROPOVHF.db, um banco de dados SQLite em um arquivo. Em memória, apenas os últimos 60 minutos permanecem para a lista e mapa; o arquivo retém tudo e serve para o historiador e estatística (também as decodificações UDP), por exemplo, as aberturas para banda e por dia, as distâncias máximas, os quadrados alcançados, e a comparação traseira entre o índice esperado e os pontos reais. As tiras descartadas do filtro são contadas mas não arquivam.

## 12 bis. Instalação automática e atualizações

A partir da versão 1.0 o programa já baixa pronto a partir da página Release de github.com/ic8tem/LIVETROPOVHF, nos mesmos pacotes de YA-MS: instalador para Windows (64 bits, 32 bits e ARM, instalar em C:\LIVETROPOVHF sem direitos de administrador) e zip portátil; para Windows 7 SP1 e 8.1 pacotes separados (-janelas7-x64 e -janela.deb,.rpm, Arch, Slackware, AppImage e tar.gz para Linux; pacotes para Raspberry Pi (64 e 32 bits) e Alpine;.dmg e apps.zip para Mac (Apple Silicon e Intel). Com os pacotes Linux, o programa é copiado para o primeiro início em ~/LIVETROPOVHF, então configurações e dados permanecem ao lado do programa; no Mac os dados nascem ao lado do LIVETROPOVHF.app.

A cada 30 minutos (o primeiro controle alguns segundos após o início, imediatamente se a última vez que o programa tinha fechado por um erro) o programa lê sozinho, sem pedir nada, o último lançamento. Se a versão for mais recente, baixe o pacote portátil adequado para o sistema e substitua os arquivos do programa no local: aqueles em uso são renomeados *.old-update e excluídos no próximo reinício. Configurações, arquivo, pasta de dados e C ALL3. TXT não são tocados. A nova versão está ativa a partir do próximo início: na instalação terminada, uma janela se propõe a reiniciar imediatamente; para correções urgentes, marcadas como [obrigatórias] no Release, a reinicialização não pode ser adiada. O status de controle e a versão em uso são escritos na parte inferior da barra de status; para desligar atualizações=0 no LIVETROPOVHF.ini. No Mac o aplicativo é assinado e não se altera: o programa apenas relata que há uma nova versão.

A imagem LIVETROPOVHF com o número de versão aparece no início; no final o programa pede confirmação e obrigado pelo uso.

## 12 ter. Barra de registro e status de erro

Qualquer erro imprevisto termina em Data/errori.log, mesmo com o log de cálculo desligado: data e hora, versão, sistema, guia aberta, erro completo e últimas linhas do que o programa estava fazendo. Um erro de interface não fecha mais o programa: na barra de status aparece "ano anotado erro" (clique em: abra o registro). No próximo início, o programa pergunta se deve enviar o relatório: abre o navegador em um sinal GitHub já compilado, com indicação, localizador e nome de usuário do PC removido, que você verifica e envia com uma conta GitHub gratuita. O log também termina com cada verificação de atualização.

Na barra de estado, as bolas de código (DX cluster, RBN, PSKReporter, MQTT, UDP) são clicadas e trazidas para a fonte; o gibi diz por que uma fonte está desligada: porto rejeitado, nó de cluster estacionado no lado do servidor, sem resposta, ou escuta UDP, mas sem pacotes de WSJT-X, JTDX ou MSHV. Um clique no número da versão imediatamente verificar atualizações.

## 13. Plataformas

livrarias externas: Avalonia para a interface [12], MQTTnet como cliente MQTT para o fluxo em tempo real do PSKReporter [13], Microsoft. Data.Sqlite para o arquivo [14].

C# em.NET 10 (LTS) [15] com interface Avalonia, publicado como um executável independente para Windows 64 e 32 bits, Mac Intel e Apple Silicon, Linux Debian e Raspberry Pi (64 e 32 bits). Limites Notados: Windows 10/11; macOS 14 ou posterior, com aplicativos para assinar ou abrir à direita → Abrir; Framboesa de Pi 2 / Zero 2 W up (modelos ARMv6 não são suportados por.NET).

## Obrigado.
Graças ao I5MZY Massimo Zannoni para testar no Windows 7 e Windows 8.

Estanques: ITU-R P.453, produtos digitais do Grupo de Estudo 3 (© ITU), utilizados para fins pessoais e não comerciais.

## Fontes principais

1. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, apresentação na 41a Reunião Nórdica VUSHF, Skjeberg 2019. Caminhos Troposféricos, Lista de Controle de Eventos (previsão Hepburn/ F5LEN, perfis de alta temperatura com Windy, DX cluster como prova de abertura), observação de que apenas mapas de previsão não são suficientes para rotas extremas.
2. J. N. Gannaway G3YGF, “Propagação de Dispersão Troposférica”, QST Novembro de 1983 (do artigo epônimo sobre RadioCommunication, RSGB, Agosto de 1981). Formulários de perda e difusão de espaço livre, raio equivalente 8497 km, termo local de 10 dB por horizonte, termo climático 0,2 · (N − 310), capacidade da planta e relação sinal/ruído, exemplo a 144 MHz.
3. W. R. Hepburn, DX Info Centre, “Tropospheric Ducting Preview” (dxinfocentre.com/tropo.html) e “The Hepburn Tropo Index (HTI)”. Escala de índice e significado do valor, predição em 6 dias.
4. F5LEN, previsão para a Europa (tropo.f5len.org/previews-for-European: list of images in the page script, class legend Disturb... Duto na imagem) e previsão mundial (tropo.f5len.org/WW) e nota “Nova versão da ferramenta mundial de previsão de propagação troposférica VHF”. Índice baseado na reatividade na unidade N, 40 passos de 3 horas, atualização 4 vezes ao dia.
5. mqtt.pskreporter.info, distribuição MQTT em tempo real dos comerciais de pskreporter.info (curado por M0LTE, fluxo de P. Gladstone usado com permissão). Portas 1883-1886, pskr/filter/v2 esquema tópico.
6. Resumo do projeto de 8/11/2026 e teste Go (core.go, gui.go), fornecido pelo autor do programa. Regras de anúncios, filtro negativo, segmentos de largura de banda, gerenciamento de clusters telnet, temas, idiomas e portabilidade.
7. C ALL3.TXT, banco de dados de indicações → JTDX landlord (I. UA3DJY, A. ES1JA script, JTAlert/hamspots.net dados). Formato do arquivo e contagem dos nomes verificados no arquivo do projeto.
8. WSJT-X, código fonte, arquivo NetworkMessage.hpp (descrição do protocolo UDP: Heartbeat, Status, Decode). Referência geral, não anexada; JTDX e MSHV usam o mesmo formato.
9. Rede de Beacon Reverso (reversebeacon.net), páginas de serviço do nó telnet. Portos 7000 (CW, RTTY) e 7001 ( FT8), login apenas com o nome.
10. D. Mills, RFC 4330 “Simples Network Time Protocol (SNTP) Version 4”, IETF 2006. Formato do pacote, cálculo de resíduos e atraso no retorno. Referência geral, não anexa.
11. NTP Pool Project (ntppool.org); INRIM, NTP time service (ntp1/ntp2.inrim.it); PTB (ptbtime1.ptb.de). Servidores usados para controle de tempo. Referência geral, não anexa.
12. Avalonia UI, documentação oficial (docs.avaloniaui.net). Interface multiplataforma, temas e layout da direita para a esquerda. Referência geral, não anexa.
13. MQTTnet, biblioteca.NET para MQTT (github.com/dotnet/MQTTnet). Cliente usado para o fluxo PSKReporter. Referência geral, não anexa.
14. SQLite (sqlite.org) e Microsoft. Data.Sqlite (learn.microsoft.com). Arquivar em um arquivo. Referência geral, não anexa.
15. Microsoft,.NET 10 – sistemas operacionais suportados e publicação autônoma em um arquivo (learn.microsoft.com/dotnet). Plataformas e limites indicados na secção 13. Referência geral, não anexa.
16. ECMA-376 “Office Open XML File Formats”, parte 1, seção 22.1 (Office Math Markup Language). Estrutura do arquivo.docx e equações. Referência geral, não anexa.
17. Lista das 28 línguas da wiki YA-MS, fornecida pelo autor do programa.
18. Open-Meteo, API de previsão meteorológica (open-meteo.com), com dados do modelo NOAA GFS. Pressão ao nível do mar para isobare (ver documento 2).
19. J. P. Snyder, “Projeções de mapa – um manual de trabalho”, USGS Professional Paper 1395, 1987 e métodos de transformação polinomial padrão para georreferenciamento de imagens. Referência geral, não anexa.
20. O Almanaque Astronómico, secção D, “Formulas de baixa precisão para a Lua” (posição de precisão de cerca de 0,3°). Usado para reconhecer conexões EME. Referência geral, não anexa.
