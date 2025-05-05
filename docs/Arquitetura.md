# DOCUMENTAÇÃO DE ARQUITETURA - PROJETO MERCADO D.

## 1. Estratégia (Strategy)

### 1.1 Motivação do Projeto
O projeto **"Mercado D."** surgiu da necessidade estratégica de transformar digitalmente a gestão financeira das lojas da rede. \
Historicamente, cada unidade operava com controles manuais ou sistemas distintos, dificultando o fechamento contábil, a padronização e a visibilidade operacional. \
A transformação visa centralizar e modernizar os lançamentos financeiros e a consolidação de saldos em uma solução escalável baseada em nuvem.

### 1.2 Hipóteses de Inovação e Valor

- Redução de 80% nos erros de lançamento com digitalização.
- Redução de 75% no tempo de fechamento semanal contábil.
- Abertura de novas lojas com 50% menos esforço de TI.
- Integração nativa com sistemas fiscais, reduzindo o retrabalho.

### 1.3 Personas e Atores
- Caixa: registra lançamentos diários.
- Gerente: analisa saldos e emite relatórios.
- Sistema externo: realiza integrações fiscais via API.

### 1.4 Objetivos de Negócio
- Reduzir custos operacionais e retrabalho.
- Ampliar a transparência da operação para a matriz.
- Criar uma fundação tecnológica para escalar a operação.

### 1.5 Resultados Esperados
- SLA superior a 99,9% nos serviços financeiros
- Integrações operacionais com baixo acoplamento
- APIs documentadas e reutilizáveis

### 1.6 Equilíbrio de Portfólio e Justificativa Comercial
A iniciativa foi classificada como estratégica e fundacional no portfólio de TI da organização. Embora não gere receita direta, cria a base para ganhos operacionais e futuras integrações (fiscal, estoque, vendas). O sucesso será medido pelo aumento da produtividade das lojas e pela capacidade de expansão sustentada.

### 1.7 Estratégia Técnica de Inovação

- Orquestração local via Aspire para fomentar prototipação
- Uso de Azure Dev/Test para validação e testes de valor
- Feature flags para ativar funcionalidades de forma gradual
- Observabilidade voltada a métricas de negócio


## 2. Planejamento (Plan)

### 2.1 Avaliação do Patrimônio Digital (Digital Estate)
Foi realizado um levantamento inicial do patrimônio digital da organização com apoio de entrevistas e inventário de sistemas existentes.
Foram classificados dois tipos principais de ativos:

**Alvos de Modernização:**
- Planilhas financeiras locais e ferramentas caseiras, substituídas pela nova plataforma
- Consolidações feitas por scripts manuais e macros Excel

**Oportunidades de Inovação:**

- Integração futura com ERP da matriz
- Conector fiscal para prefeituras e Receita Federal

**O modelo de racionalização utilizado foi baseado nos 5Rs:**

**- Rehost, Replace:** nenhuma aplicação
**- Refactor:** APIs e microsserviços construídos do zero
**- Rearchitect:** planejamento modular para expansões futuras
**- Rebuild:** Reconstruir a aplicação financeira do zero usando Azure

### 2.2 Alinhamento Organizacional
A adoção de cloud exige capacitação e mudança de cultura. Abaixo as áreas e responsáveis:
- Equipe de Engenharia: responsável pela automação, infraestrutura como código e pipelines
- TI da Matriz: responsável pela homologação da governança e segurança
- Gerentes Regionais: pilotos do rollout por loja
Esses atores integram o time de estratégia de nuvem, que será responsável por liderar a implantação.

### 2.3 Plano de Capacitação
Para garantir o sucesso da adoção, foram definidos treinamentos:

|Curso	|Público-alvo	|Nível	|Fonte	|Prioridade|
|---	|---	|---	|---	|---|
|Fundamentos do Azure				|TI, Devs, Gerentes TI	|100	|MS Learn	|	Alta	|
|CAF: Valor de Negócio				|Gestores, C-Level		|100	|MS Learn	|	Média	|
|Práticas de DevOps com Azure		|Devs, Eng. Software	|200	|Pluralsight|	Alta	|
|.NET Aspire para desenvolvimento	|Devs .NET				|200	|MS Learn	|	Alta	|

### 2.4 Primeiro Projeto de Adoção
O primeiro projeto será a implantação do sistema de controle de lançamentos e saldo consolidado como prova de valor.
- Projeto: MVP da plataforma financeira distribuída
- Stakeholder: Diretoria Financeira
- Unidade de Negócio: Rede de Lojas (Operações)
- Resultado esperado: reduzir o tempo de fechamento contábil semanal de 2 dias para 2 horas
Essa iniciativa responde diretamente às motivações de padronização, visibilidade em tempo real e redução de custos com retrabalho.


## 3. Governança e Gerenciamento (Govern & Manage)

### 3.1 Modelo de Governança
Adotamos uma governança baseada nos princípios do Cloud Adoption Framework, com foco em controle de custos, segurança e consistência. Utilizamos recursos como:

- Azure Policy: para enforcing de conformidade
- Azure Management Groups: segmentação por ambientes (dev, prod)
- RBAC com Azure AD: acesso baseado em função e nível de responsabilidade

A governança é sustentada por um comitê estratégico com representantes da TI, do negócio e da operação.

### 3.2 Segurança e Compliance
- Azure Defender for Cloud: proteção proativa
- Key Vault: gestão segura de segredos
- Monitoramento de ameaças e comportamentos anômalos com alertas automatizados
- Criptografia em repouso e em trânsito habilitada por padrão

### 3.3 Monitoramento, Recuperação e Operações
- Azure Monitor + Log Analytics: centralização de logs e métricas
- Workbooks personalizados para acompanhamento de KPIs técnicos e de negócio
- Azure Backup e Site Recovery: estratégias de continuidade de negócios
- Testes trimestrais de DR com análise de lições aprendidas

### 3.4 Indicadores de Governança
- 100% dos recursos com tagging obrigatório
- 100% dos segredos armazenados com rotação automática
- Menos de 5% de desvios de política identificados por mês
- SLA operacional de 99,9% nos serviços de missão crítica


## 4. Arquitetura da Solução (Architecture)

### 4.1 Visão Geral da Arquitetura
A arquitetura do sistema Mercado D. adota o modelo orientado a eventos com componentes desacoplados, possibilitando escalabilidade horizontal, resiliência a falhas e observabilidade em tempo real. A solução foi projetada para rodar prioritariamente em uma única região Azure com uso intensivo de zonas de disponibilidade, e failover para uma região secundária via Azure Site Recovery.

### 4.2 Componentes Principais
- **Azure API Management:** gerenciamento e exposição de APIs públicas e internas
- **Azure App Service (Web API):** APIs de lançamento e consulta, implementadas em .NET 8
- **Azure SQL Database:** armazenamento transacional dos lançamentos com replicação geo-reduntante
- **Azure Service Bus:** mensageria assíncrona entre APIs e processamento de consolidação
- **Azure Functions:** workers para consolidação diária e geração de relatórios
- **Azure Cache for Redis:** fornecimento rápido dos dados consolidados
- **Azure Key Vault:** gerenciamento de segredos e conexões
- **Application Insights:** rastreamento distribuído

### 4.3 Diagrama Lógico
O diagrama lógico representa a interação dos componentes mencionados acima com fluxo entre:

1.	O ator (caixa) realiza um lançamento via aplicativo (mobile/web)
2.	O front-end chama a API de Lançamentos exposta via API Management
3.	A API grava em Azure SQL e publica evento de domíno no Service Bus
4.	A Azure Function consome o evento e atualiza a consolidação no Redis
5.	O gerente ou sistema externo consulta os dados consolidados via API
[Diagrama lógico será incluído na versão final com Visio ou Draw.io]

### 4.4 Diagrama de Casos de Uso (UML)
Atores:
- **Financeiro:** interage com a API para registrar lançamentos
- **Gerente:** consulta saldos consolidados, exporta relatórios
- **Sistema Fiscal:** consome API externa para integração tributária

Casos de uso principais:
- Registrar Lançamento
- Consultar Saldo Consolidado
- Exportar Relatório Financeiro
- Integrar com Sistema Externo
[Diagrama de casos de uso UML será anexado à entrega final visualmente]

### 4.5 Justificativas Arquiteturais
- Alta disponibilidade: App Services e bancos com replicação em zonas de disponibilidade
- Baixo acoplamento: arquitetura assíncrona com fila entre módulos
- Escalabilidade independente: Redis e Azure Functions escalam conforme carga
- Desenvolvimento local e modular: .NET Aspire com múltiplos projetos coordenados via dashboard

