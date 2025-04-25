namespace MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro
{
    public sealed class CreateLancamentoFinanceiroCommandResponse
    {
        public Guid Id { get; set; }

        public CreateLancamentoFinanceiroCommandResponse(Guid id)
        {
            Id = id;
        }
    }
}
