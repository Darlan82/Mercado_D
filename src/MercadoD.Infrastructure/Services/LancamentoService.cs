namespace MercadoD.Infrastructure.Services;

using System.Text.Json;
using Azure.Messaging.ServiceBus;
using MercadoD.Application.Services;
using MercadoD.Domain.Entities;
using MercadoD.Infrastructure.Data;

/// <summary>
/// Implementação do serviço de lançamentos financeiros.
/// </summary>
public class LancamentoService : ILancamentoService
{
    private readonly MercadoDbContext _dbContext;
    private readonly ServiceBusSender _serviceBusSender;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="LancamentoService"/>.
    /// </summary>
    /// <param name="dbContext">Contexto de dados.</param>
    /// <param name="serviceBusSender">Enviador de mensagens do Service Bus.</param>
    public LancamentoService(MercadoDbContext dbContext, ServiceBusSender serviceBusSender)
    {
        _dbContext = dbContext;
        _serviceBusSender = serviceBusSender;
    }

    /// <inheritdoc/>
    public async Task RegistrarLancamentoAsync(LancamentoFinanceiro lancamento)
    {
        _dbContext.Lancamentos.Add(lancamento);
        await _dbContext.SaveChangesAsync();

        var payload = JsonSerializer.Serialize(lancamento);
        var message = new ServiceBusMessage(payload);
        await _serviceBusSender.SendMessageAsync(message);
    }
}