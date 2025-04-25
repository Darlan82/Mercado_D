namespace MercadoD.Domain.Loja.FluxoCaixa
{
    public class LancamentoFinanceiro : EntityBase
    {
        public Guid ContaId { get; protected set; }
        public ContaFinanceira Conta { get; protected set; } = null!; // Fix for CS8618

        public decimal Valor { get; protected set; }

        public string Descricao { get; protected set; }

        public DateTime DtLancamento { get; protected set; }
        public DateTime DtVencimento { get; protected set; }
        public DateTime? DtPagamento { get; protected set; }

        public bool SaldoPrevistoContabilizado { get; set; }
        public bool SaldoRealizadoContabilizado { get; set; }

        public LancamentoFinanceiro(Guid contaId, decimal valor, string descricao)
            : base()
        {
            ContaId = contaId;
            Valor = valor;
            Descricao = descricao ?? throw new ArgumentNullException(nameof(descricao));
        }

        public LancamentoFinanceiro(Guid contaId, decimal valor, string descricao,
            DateTime? dtLancamento = null, DateTime? dtVencimento = null)
            : this(contaId, valor, descricao)
        {
            DtLancamento = dtLancamento.HasValue ? dtLancamento.Value : this.DtCriacao;
            DtVencimento = dtVencimento.HasValue ? dtVencimento.Value : this.DtLancamento;
        }
    }
}
