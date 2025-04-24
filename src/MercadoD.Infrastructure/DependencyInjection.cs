namespace MercadoD.Infrastructure;

using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

/// <summary>
/// Extensões para registro de serviços de infraestrutura.
/// </summary>
public static class DependencyInjection
{
    private const string HealthEndpointPath = "/health";
    private const string AlivenessEndpointPath = "/alive";

    //public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    //{
    //    //builder.ConfigureOpenTelemetry();

    //    builder.AddDefaultHealthChecks();

    //    builder.Services.AddProblemDetails();
    //    builder.Services.AddServiceDiscovery();
    //    builder.Services.ConfigureHttpClientDefaults(http =>
    //    {
    //        // Turn on resilience by default
    //        http.AddStandardResilienceHandler();

    //        // Turn on service discovery by default
    //        http.AddServiceDiscovery();
    //    });

    //    // Uncomment the following to restrict the allowed schemes for service discovery.
    //    // builder.Services.Configure<ServiceDiscoveryOptions>(options =>
    //    // {
    //    //     options.AllowedSchemes = ["https"];
    //    // });

    //    return builder;
    //}

    


    //public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    //public static TBuilder AddInfrastructure<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    //{
    //    var services = builder.Services;
    //    var configuration = builder.Configuration;

    //    //// Configura DbContext com SQL Server
    //    //services.AddDbContext<MercadoDbContext>(options =>
    //    //    options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));

    //    // Configura ServiceBusClient
    //    services.AddSingleton(serviceProvider =>
    //    {
    //        var sbConnectionString = configuration["ConnectionStrings:ServiceBus"];
    //        return new ServiceBusClient(sbConnectionString);
    //    });

    //    // Configura ServiceBusSender
    //    services.AddSingleton(serviceProvider =>
    //    {
    //        var client = serviceProvider.GetRequiredService<ServiceBusClient>();
    //        var topicName = configuration["AzureServiceBus:TopicName"];
    //        return client.CreateSender(topicName);
    //    });

    //    // Registra serviço de aplicação para lançamentos financeiros
    //    //services.AddScoped<ILancamentoService, LancamentoService>();

    //    return builder;
    //}

    
}