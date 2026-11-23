LIVETROPOVHF · documento técnico 3
# Como formar o tropo em 144 MHz e além

> Tradução automática por italiano: em caso de dúvida vale o guia italiano.

O que acontece no ar quando os 144, 432 e microondas vão além do horizonte: refração, inversões, dutos, espalhamentos e como lê-los no mapa do programa. Versão de 9 de novembro de 2026.

## Em resumo

- As ondas VHF e além de viajar nos primeiros quilômetros de atmosfera (troposfera) e dobrar onde o índice de refração muda com o [3] [7] [11].
- O índice é medido com N de refração, que depende da pressão, temperatura e acima de tudo umidade [11].
- Na atmosfera normal há cerca de 40 unidades por km; quando cai mais de 157 unidades por km a onda permanece presa em um conduíte [7] [11].
- O conduíte vem de uma inversão térmica com ar seco acima e molhado abaixo: anticiclone, mar, noites serenas, frentes [3] [4] [5] [13].
- Quanto maior a frequência, mais fino o ducto é suficiente: as micro-ondas abrem mais frequentemente do que as 144 [3] [7].
- Sem conduíte permanece a dispersão tropospher: sempre presente, fraco, que cai com a distância [6].
- No programa: alta pressão + reverso + Hepburn/ F5LEN alta = condições prováveis; os pontos confirmam [1] [2] [13] [14].

## 1. Onde acontece: a troposfera

A troposfera é a camada mais baixa da atmosfera, até cerca de 10-12 km. Para a contagem trope principalmente os primeiros 3 km: há vapor de água e inversões, e lá passam as rotas de rádio entre estações no solo [4]. Em condições normais a temperatura cai com a altitude de cerca de 6.5 °C por km [3]. Quando, por outro lado, em uma camada, a temperatura sobe com a altitude, falamos de inversão térmica: é o ponto de partida de quase todas as fortes aberturas de tropo.

Os FVH não são refletidos pela ionosfera como a HF (salva e esporádica, que tem outras horas e outras distâncias [5]): além do horizonte só vem se a atmosfera os dobra para o chão ou os espalha.

## 2. Refracção e N de refração

Uma onda de rádio se dobra através do ar com um índice de refração de ar com outro. O índice n é muito próximo de 1, então a reatividade N é usada, ou seja, n − 1 multiplicado por um milhão [11]:

```math
N = (n - 1) \cdot 10^{6} = \frac{77,6}{T} \left( P + 4810 \, \frac{e}{T} \right)
```

com P a pressão na hPa, T a temperatura em kelvin e a pressão do vapor de água na hPa. N solo é geralmente 300-400. O segundo termo, o do vapor, pesa muito: por esta razão a umidade conta tanto quanto a temperatura [3] [11].

O que dobra a onda não é o valor de N, mas como ela muda com a cota, o gradiente dN/dh:

| dN/dh (N/km) | Situação | Efeito na onda |
| 0 | Subrefração | A onda afasta-se do solo, o horizonte mais curto |
| de 0 a −79 | Normal (padrão −39) | Luz curvando-se para o chão: horizonte de rádio cerca de 4/3 da óptica |
| de −79 a −157 | Superrefração | A onda dobra mais, aumento das taxas de fluxo |
| inferior a −157 | Conteúdo | A onda dobra tanto quanto a Terra ou mais e permanece presa |

O limiar de −157 N/km é o ponto onde a curvatura do raio é igual à da Terra [7] [11]. Daí o "viga terrestre equivalente" de 8497 km (4/3 do raio real) utilizado no cálculo da Dispersão [6].

## 3. Inversões: o motor tropo

Um gradiente abaixo de −157 N/km requer N para colapso em poucos metros de altitude. Acontece quando acima de uma camada de ar fresco e úmido é encontrado mais quente e seco: a temperatura sobe e o vapor cai junto, e ambos derrubar N [3] [5]. LA0BY indica inversões até 10–15 °C por km contra a diminuição normal de 6,5 °C [3].

Inversões surgem de maneiras diferentes:

- Subsidence em anticiclones. Na alta pressão o ar em altitude cai lentamente, comprime e aquece, enquanto no solo permanece ar fresco e úmido. É a causa das aberturas mais longas e longas, que duram dias [3] [4] [13].
- Radiação nocturna. Com céu sereno e vento fraco o solo esfria à noite e esfria o ar de contato: Baixo reverso que quebra com o sol da manhã. Aberturas da noite e da manhã, típicas do verão e outono.
- Aviso do mar. Ar quente e seco vindo do solo sobre um mar mais frio: a camada do mar permanece úmida e fria abaixo. A partir daqui as grandes rotas marítimas (Atlântico, Mediterrâneo, Mar do Norte) [4] [13].
- Frentes. Na frente de uma frente quente o ar quente flui sobre o frio; a abertura é curta e segue a frente.
- Evaporação. Acima do mar, nos primeiros metros, a umidade cai rapidamente com a altitude: ducto muito fino, útil apenas para microondas.

O sinal de que um reverso é visto nos perfis verticais (radiosondaggi): temperatura subindo e orvalho descendo no mesmo trecho [5]. O programa mostra uma pista disso com a camada de inversão térmica (ar a 925 hPa mais quente do que no solo) e com a isotérmica a 850 hPa.

## 4. Dutos de superfície e de altura

Um conduíte é uma camada na qual a onda viaja saltando entre a borda superior (inversão) e a borda do solo ou inferior [7]:

- Oleoduto de superfície: terra ou mar. As estações de baixa altitude, especialmente na costa, entram diretamente.
- Alto oleoduto: está em altitude, por exemplo entre 500 e 2000 m. As estações nas planícies podem não sentir, enquanto as que estão na colina ou na montanha entram no conduíte. Explique as ligações que atravessam os Alpes ou outros [3] relevos [4].

Para entrar no conduíte a onda deve começar quase horizontal: antenas baixas no horizonte e nenhum obstáculo vizinho. Dentro do conduíte a atenuação é muito menor do que no espaço livre, e as distâncias atingem 1000-2000 km e mais além (por exemplo, as conexões entre a Península Ibérica, as Ilhas Canárias e Inglaterra, ou entre a Escandinávia e o sul da Rússia) [3].

## 5. Frequência: por que micro-ondas abrem mais frequentemente

Um conduíte apenas prende ondas bastante curtas em comparação com a sua espessura. As ondas longas “saem” de um duto fino; as curtas não saem. Então [3] [7]:

| Banda | Comprimento de onda | Do que precisas? |
| 144 MHz |  2 m  | Oleodutos grossos (centinaia de metros), inversões largas e bem marcadas: aberturas menos frequentes, mas muito extensas |
| 432 MHz | 70 cm | Oleodutos mais finos: muitas vezes abre quando 144 são apenas “bens” |
| 1296 MHz | 23 cm | Poucas inversões fracas; rotas longas no mar freqüentes no verão |
| 10 GHz | 3 cm | Apenas o ducto de evaporação no mar; também conta com a absorção da chuva |

Por isso, um modesto índice de tropo já pode significar aberturas em UHF e micro-ondas, enquanto os 144 exigem valores mais elevados. Por outro lado, na mesma conduta, os 144 têm menos perdas fora da conduta e melhor cobrir as áreas fronteiriças.

## 6. A dispersão Troposfer

Mesmo sem inversão, a troposfera não é uniforme: Pequenas irregularidades de temperatura e umidade espalham uma pequena parte da energia para além do horizonte. É a dispersão troposférica, sempre presente, que permite conexões de 300-800 km com boas estações [6]. O sinal é fraco e flutuante; a perda cresce com o ângulo de difusão, ou seja, com a distância e com os obstáculos no horizonte:

```math
L_{s} = 21 + 10\, \theta_{s} + 10 \log f
```

com o ângulo de difusão em graus e f em MHz [6]. O programa usa esta fórmula na aba de cada ponto para dizer se um link pode ser explicado com o espalhamento ou requer um conduíte (ver documento 1). Um alto valor de N no solo melhora a dispersão [6].

Não confundir com o airscatter (reflexão em planos: sinais de alguns segundos, Doppler) e o meteoro de dispersão (reflexão em meteoros), que o programa exclui do tropo.

## 7. O quadro meteorológico: como lê-lo no mapa

As aberturas do tropo seguem a meteorologia. Situações típicas [3] [4] [13]:

- Anticiclona estável, alta pressão (mais de 1020-1025 hPa), isobare largo, pouco vento: inversão de subsidência. As melhores aberturas seguem a borda do anticiclone, especialmente no mar.
- Calo della pressionione: quebras de inversão. G4MCU seguiu o farol SK6VHF até a pressão cair para 1012 hPa [13].
- Ar quente em altitude (isoterme a 850 hPa subindo) sobre o ar fresco no solo: inverter.
- As células convectivas e as ondas de gravidade (por exemplo, o vento dos Alpes) podem criar aberturas locais e curtas [4].
- Hora do dia: as inversões da irradiação são mais fortes à noite e no início da manhã; a subsidência permanece também de dia.

Os índices Hepburn [1] e F5LEN [2] juntaram tudo isso a partir de modelos meteorológicos: Eles calculam N em várias quotas e transformam gradientes em um número ou classe. Eu não sou uma garantia: LA0BY mostra um evento LA- UA6 que é visível em alta temperatura e não em Hepburn [3]. Para isso o programa mostra em conjunto índices, isobare, temperaturas e pontos reais: se os pontos se alinharem a uma área de alto índice sob um anticiclone, a abertura está em andamento. KP4MD encontrou o SNR de relatórios WSPR para 144 MHz crescendo direito nas janelas fornecidas por Hepburn [14].

## 8. Lista prática

1. Olhe para o índice tropo em sua área e ao longo da direção que lhe interessa, para as próximas horas.
2. Verificar isobare: alta pressão e ampla isobare sobre ou ao lado do caminho.
3. Ligar a inversão térmica e T 850 hPa: ar quente a altitude acima do caminho.
4. Prefere rotas no mar, à noite e de manhã.
5. Se você estiver na colina, pense nos dutos altos; Se estiver na costa, na superfície.
6. Tente primeiro as bandas mais altas (432, 1296): abra primeiro.
7. Verificar com ponto e PSKReporter: linhas longas na mesma direção confirmar o ducto.

## Fontes

1. W. R. Hepburn, DX Info Centre, “The Hepburn Tropo Index (HTI)” e “Tropospheric Ducting Preview” (dxinfocentre.com).
2. F5LEN, previsão para a Europa e previsão mundial de propagação tropoférica VHF (tropo.f5len.org).
3. S. Heck LA0BY, “Extreme Tropo Propagation on 144 MHz and up”, 41° Nordic VUSHF Meeting, Skjeberg 2019. Gradiente normal e inversões, temperatura e umidade, altas pressões, evento LA- UA6, conexões além das montanhas.
4. F. Egano IK3XTV, “Propagação VHF – A contribuição de células convectivas e ondas gravitacionais atmosféricas na propagação Tropo.” Primeiros 3 km, caminhos marinhos, células convectivas, ondas de gravidade, alta pressão.
5. V. Grassmann DF5AI, U. Langenohl DK5YA, “Propagação de Distância Muito Longa na Banda de 144 MHz”, 24/7/2003. Perfis de temperatura e ponto de orvalho, inversões, distinção de E esporádico.
6. J. N. Gannaway G3YGF, “Propagação de Dispersão Troposférica”, QST Novembro 1983. Raio da Terra equivalente e perda de dispersão.
7. O livro VHF/UHF DX, capítulo 2 “Propagação Troposférica”. Zonas de refração de acordo com os perfis dN/dh e de superfície e de conduta elevada (apenas as figuras).
11. Recomendação ITU-R P.453, índice de refração por rádio. Fórmula N e limiar do canal a −157 N/km. Referência geral, não anexa.
13. G4MCU, artigos “troposféricos” (g4mcu.net), 2018-2025. Condotti com alta pressão, especialmente no mar; SK6VHF e diminuir para 1012 hPa.
14. C. F. Milazzo KP4MD, “VHF/UHF WSPR Propagation Study”, 2012-2014. SNR WSPR a 144 MHz e previsão de Hepburn.

Os números de origem são os mesmos que o documento 2, Map Building.
