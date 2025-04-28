// Orchestrator for MercadoD solution using .NET Aspire
using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Provisions a containerized SQL Server database when published
IResourceBuilder<SqlServerServerResource> sqlServer;

var sb = builder.AddAzureServiceBus("servicebus");               

//if (Debugger.IsAttached)
if (builder.Environment.IsDevelopment())
{
    var senha = builder.AddParameter("pwdSql", "S3nh@F0rte123!");
    sqlServer = builder.AddSqlServer("sqlserver", senha, port: 5433);

    sb = sb.RunAsEmulator();
}
else
{
    sqlServer = builder.AddSqlServer("sqlserver");
}

var dbMercadoD = sqlServer.WithDataVolume()
                    .AddDatabase("mercadoD");

// Console one-shot que aplica as migrações e encerra
var migrator = builder
        .AddProject<Projects.MercadoD_Infra_Persistence_Sql_JobMigration>("migrator")
        .WithReference(dbMercadoD)                                   // injeta CS
        .WithEnvironment("DOTNET_ENVIRONMENT", builder.Environment.EnvironmentName) // -> Development
        .WaitFor(dbMercadoD);                                        // só roda se o DB estiver healthy


var api = builder.AddProject<Projects.MercadoD_API>("api")
    .WithReference(dbMercadoD).WaitFor(dbMercadoD) //Referencia o banco e espera o banco ficar online
    .WithReference(sb).WaitFor(sb)
    .WaitForCompletion(migrator) // espera a aplicação da migração
    ;

var app = builder.Build();
app.Run();
