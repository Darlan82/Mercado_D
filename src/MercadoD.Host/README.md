
### Infraestrutura

Este projeto é um Host do .Net Aspire.
Ele provisiona o orquestra toda infra estrutura localmente.
Para rodar é necessário ter o docker e estar rodando.

### Banco de dados

O Host do Apira provisionará uma imagem do `SQL Server` via `Docker`. \
Para acessar o banco atravez do `SQL Server Management Studio` você conect com usuário `sa` 
a senha vc pode obter atravez do painel do Aspire na env `MSSQL_SA_PASSWORD` ou via linha de comando 
`dotnet user-secrets list` na saída `Parameters:sqlserver-password`. 
O endereço do banco será levantado em `tcp:127.0.0.1,porta` e a porta é criada dinamicamente. 
A porta vc pode pegar no painel do Aspire ou via comando `docker ps --filter name=sql` 
na saída da coluna `PORTS` da tabela exemplo: \

| CONTAINER ID		| IMAGE											| COMMAND					| CREATED			| STATUS			| PORTS							| NAMES					| 
| ---				| --											| --						| --				| --				| --							| --					|
| aa02d5bc8dcf		| mcr.microsoft.com/mssql/server:2022-latest	| "/opt/mssql/bin/perm…"	| 51 minutes ago	| Up 51 minutes		| 127.0.0.1:53028->1433/tcp		| sqlserver-ygcdhgvf	|

Nessa caso o endereço fica `tcp:127.0.0.1,53028`.

#### Criação de Base de dados e população dos dados

O Aspire executa uma tarefa de migração que está no projeto `MercadoD.Persistence.Sql` automaticamente.
A migração cria as tabelas e somente popula os dados para o desenvolvedor no ambiente de desenvolvimento e
sua definição está na classe `DbDevInitializer` no projeto `MercadoD.Persistence.Sql`.

Para saber como trabalhar com a migração veja o documento [MercadoD.Persistence.Sql/README.md](../MercadoD.Persistence.Sql/README.md).
