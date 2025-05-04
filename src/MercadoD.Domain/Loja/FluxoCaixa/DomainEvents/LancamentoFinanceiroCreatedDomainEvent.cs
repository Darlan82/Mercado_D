namespace MercadoD.Domain.Loja.FluxoCaixa.DomainEvents
{
    public record LancamentoFinanceiroCreatedDomainEvent : IDomainEvent
    {
        public Guid Id { get; init; }
        public Guid ContaId { get; init; }
    }        
}
