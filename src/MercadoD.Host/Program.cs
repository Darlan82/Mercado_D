// Orchestrator for MercadoD solution using .NET Aspire
using StackExchange.Redis;

var builder = DistributedApplication.CreateBuilder(args);

// Provisions a containerized SQL Server database when published
var sqlServer = builder.AddSqlServer("sqlserver")                        
                        .WithDataVolume()
                        .AddDatabase("mercadoD");

// 1️⃣  Console one-shot que aplica as migrações e encerra
var migrator = builder
    .AddProject<Projects.MercadoD_Persistence_Sql_Migration>("migrator")                                                                   
    .WithReference(sqlServer)                                   // injeta CS
    .WithEnvironment("DOTNET_ENVIRONMENT", builder.Environment.EnvironmentName) // -> Development
    .WaitFor(sqlServer);                                        // só roda se o DB estiver healthy

// Redis container
//var cache = builder.AddRedis("cache");

var api = builder.AddProject<Projects.MercadoD_API>("api")
    .WithReference(sqlServer).WaitFor(sqlServer) //Referencia o banco e espera o banco ficar online
    .WaitFor(migrator) // espera a aplicação da migração
    ;

//// Redis container
//builder.AddContainer("redis", container => container
//    .UseImage("redis:7-alpine")
//    .ExposePort(6379));

//// Enable the Aspire dashboard for basic metrics
//builder.EnableDashboard();

//// Configure OpenTelemetry tracing for the orchestrator
//builder.Services.AddOpenTelemetryTracing(tracerBuilder =>
//{
//    tracerBuilder
//        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MercadoD.Host"))
//        .AddAspNetCoreInstrumentation()
//        .AddHttpClientInstrumentation()
//        .AddSqlClientInstrumentation()
//        .AddConsoleExporter();
//});

//// Configure OpenTelemetry metrics for the orchestrator
//builder.Services.AddOpenTelemetryMetrics(meterBuilder =>
//{
//    meterBuilder
//        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MercadoD.Host"))
//        .AddAspNetCoreInstrumentation()
//        .AddHttpClientInstrumentation()
//        .AddConsoleExporter();
//});

var app = builder.Build();
app.Run();
