namespace MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro
{
    public sealed class CreateLancamentoFinanceiroCommand
    {
        public Guid ContaId { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime? DtVencimento { get; set; }

        public CreateLancamentoFinanceiroCommand(Guid contaId, decimal valor, string descricao, DateTime? dtVencimento)
        {
            ContaId = contaId;
            Valor = valor;
            Descricao = descricao ?? throw new ArgumentNullException(nameof(descricao));
            DtVencimento = dtVencimento;
        }
    }
}
