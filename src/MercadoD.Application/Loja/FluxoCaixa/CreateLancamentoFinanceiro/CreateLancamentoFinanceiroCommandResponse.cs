namespace MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro
{
    public sealed record CreateLancamentoFinanceiroCommandResponse(Guid Id);

    public sealed record CreateLancamentoFinanceiroCommandResponseError(string ErrorMessage);
}
