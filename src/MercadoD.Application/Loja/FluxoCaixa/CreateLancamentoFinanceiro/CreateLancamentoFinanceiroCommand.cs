namespace MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro
{
    public sealed record CreateLancamentoFinanceiroCommand(Guid ContaId, decimal Valor, string Descricao,
        DateTime? DtVencimento);
}
