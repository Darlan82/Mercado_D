namespace MercadoD.Domain.Loja.FluxoCaixa.DomainEvents
{
    public sealed record LancamentoFinanceiroCreatedDomainEvent(Guid id, Guid ContaId) : IDomainEvent;
}
