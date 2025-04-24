param(
    [string]$Name = $("Mig_" + (Get-Date -Format "yyyyMMddHHmmss"))
)

dotnet ef migrations add $Name `
  --context MercadoEFContext `
  --project MercadoD.Persistence.Sql/MercadoD.Persistence.Sql.csproj `
  --startup-project MercadoD.Persistence.Sql.Migration/MercadoD.Persistence.Sql.Migration.csproj