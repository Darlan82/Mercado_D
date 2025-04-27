using MercadoD.Common.Time;

namespace MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro
{
    public sealed record CreateLancamentoFinanceiroCommand
    {
        public Guid ContaId { get; init; }
        public decimal Valor { get; init; }
        public string Descricao { get; init; }
        public DateTime? DtLancamento { get; init; }
        public DateTime? DtVencimento { get; init; }
        public DateTime? DtPagamento { get; init; }

        public CreateLancamentoFinanceiroCommand(Guid contaId, decimal valor, string descricao, 
            DateTime? dtLancamento, DateTime? dtVencimento, DateTime? dtPagamento)
        {
            ContaId = contaId;
            Valor = valor;
            Descricao = descricao;
            DtLancamento = dtLancamento.GetValueOrDefault(Clock.UtcNow);
            DtVencimento = dtVencimento;
            DtPagamento = dtPagamento;
        }
    }
}
