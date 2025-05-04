// Orchestrator for MercadoD solution using .NET Aspire
using Aspire.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Provisions a containerized SQL Server database when published
IResourceBuilder<SqlServerServerResource> sqlServer;


//if (Debugger.IsAttached)
if (builder.Environment.IsDevelopment())
{
    var senha = builder.AddParameter("pwdSql", "S3nh@F0rte123!");
    sqlServer = builder.AddSqlServer("sqlserver", senha, port: 5433);

    //var serviceBus = builder.AddConnectionString("servicebus");
}
else
{
    sqlServer = builder.AddSqlServer("sqlserver");
    
}

//sb.AddServiceBusQueue("lancamento-financeiro");

var dbMercadoD = sqlServer.WithDataVolume()
                    .AddDatabase("mercadoD");

// Console one-shot que aplica as migrações e encerra
var migrator = builder
        .AddProject<Projects.MercadoD_Infra_Persistence_Sql_JobMigration>("migrator")
        .WithReference(dbMercadoD)                                   // injeta CS
        .WithEnvironment("DOTNET_ENVIRONMENT", builder.Environment.EnvironmentName) // -> Development
        .WaitFor(dbMercadoD);                                        // só roda se o DB estiver healthy


var api = builder.AddProject<Projects.MercadoD_API>("api")
    .WithReference(dbMercadoD).WaitFor(dbMercadoD)     
    .WaitForCompletion(migrator) // espera a aplicação da migração
    ;

if (builder.Environment.IsDevelopment())
{
    //var connSb = builder.Configuration.GetConnectionString("servicebus");
    var serviceBus = builder.AddConnectionString("servicebus");
    api.WithReference(serviceBus);
}
else
{
    var sb = builder.AddAzureServiceBus("servicebus");
    api.WithReference(sb)
        .WaitFor(sb);
}

var app = builder.Build();
app.Run();
