

O Host provisiona uma imagem do `SQL Server` via `Docker`. \
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
