
## Instruções para da manutenção no banco de dados

### 1. Prérequisitos 

Esse projeto tem migração do Entity Framework Core 9.
Para dar manutenção nas migrações é necessário ter o `dotnet ef` instalado. \
Para ferificar se tem o `dotnet ef` instalado , execute o comando: \
`dotnet ef --version` \
Caso não tenha, instale com o comando: \
`dotnet tool update -g dotnet-ef --version 9.*` 

### 2. Atualizar de Migração

#### 2.1 Adiconar uma nova migração
Abra um terminal em PowerSell na pasta `src` e execute o script ` .\AddMigrations.ps1`.
Ele vai adicionar uma nova migração com sufixo `Ming_yyyyMMddHHmmss`.

