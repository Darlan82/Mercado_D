dotnet ef migrations add remove  `
  --context MercadoEFContext `
  --project MercadoD.Persistence.Sql/MercadoD.Persistence.Sql.csproj `
  --startup-project MercadoD.Persistence.Sql.Migration/MercadoD.Persistence.Sql.Migration.csproj