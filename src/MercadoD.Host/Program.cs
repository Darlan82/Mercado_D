// Orchestrator for MercadoD solution using .NET Aspire
using Azure.Provisioning.ServiceBus;

var builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<IResourceWithConnectionString> dbMercadoD;

const string sqlName = "sqlserver", dbName = "mercadoD";

if (!builder.ExecutionContext.IsPublishMode)
{
    var senha = builder.AddParameter("pwdSql", "S3nh@F0rte123!");
    dbMercadoD = (IResourceBuilder<IResourceWithConnectionString>)
    builder.AddSqlServer(sqlName, senha, port: 5433) // Provisions a containerized SQL Server database when published
      .WithDataVolume() //Opicional - Mantem os dados entre as builds
      .AddDatabase(dbName);
}
else
{
    dbMercadoD = builder.AddAzureSqlServer(sqlName)                        
                       .AddDatabase(dbName);
}

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


const string domainMessageTypeInfra = "DomainMessageTypeInfra";
if (!builder.ExecutionContext.IsPublishMode)
{
    var senha = builder.AddParameter("pwdRMq", "1234");
    var username = builder.AddParameter("userRMq", "adm");

    var rabbitmq = builder.AddRabbitMQ("rabbitmq", userName: username, password: senha)
                    //.WithEnvironment("deprecated_features.permit.management_metrics_collection", "false")
                    .WithManagementPlugin()
                    .WithDataVolume(isReadOnly: false)
                    ;

    api.WithEnvironment(domainMessageTypeInfra, "RabbitMQ")
        .WithReference(rabbitmq)
        .WaitFor(rabbitmq);

    //var serviceBus = builder.AddConnectionString("servicebus");
    //api.WithReference(serviceBus);
}
else
{
    var sb = builder.AddAzureServiceBus("servicebus")
        .ConfigureInfrastructure(infra =>
        {
            var serviceBusNamespace = infra.GetProvisionableResources()
                                           .OfType<ServiceBusNamespace>()
                                           .Single();

            serviceBusNamespace.Sku = new ServiceBusSku
            {
                Name = ServiceBusSkuName.Standard
            };
            //serviceBusNamespace.Tags.Add("ExampleKey", "Example value");
        });

    api.WithEnvironment(domainMessageTypeInfra, "AzureServiceBus")
        .WithReference(sb)
        .WaitFor(sb);
}

var app = builder.Build();
app.Run();
