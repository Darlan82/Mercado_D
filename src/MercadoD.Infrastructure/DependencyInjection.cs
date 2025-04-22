namespace MercadoD.Infrastructure;

using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MercadoD.Application.Services;
using MercadoD.Infrastructure.Data;
using MercadoD.Infrastructure.Services;

/// <summary>
/// Extensões para registro de serviços de infraestrutura.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registra DbContext, Service Bus e serviços de domínio.
    /// </summary>
    /// <param name="services">Coleção de serviços.</param>
    /// <param name="configuration">Configurações da aplicação.</param>
    /// <returns>Serviços registrados.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configura DbContext com SQL Server
        services.AddDbContext<MercadoDbContext>(options =>
            options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));

        // Configura ServiceBusClient
        services.AddSingleton(serviceProvider =>
        {
            var sbConnectionString = configuration["ConnectionStrings:ServiceBus"];
            return new ServiceBusClient(sbConnectionString);
        });

        // Configura ServiceBusSender
        services.AddSingleton(serviceProvider =>
        {
            var client = serviceProvider.GetRequiredService<ServiceBusClient>();
            var topicName = configuration["AzureServiceBus:TopicName"];
            return client.CreateSender(topicName);
        });

        // Registra serviço de aplicação para lançamentos financeiros
        services.AddScoped<ILancamentoService, LancamentoService>();

        return services;
    }
}