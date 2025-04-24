using MercadoD.Domain.Entities;

namespace MercadoD.Domain.Loja.FluxoCaixa
{
    public class LancamentoFinanceiro : EntityPadrao
    {
        public Guid ContaId { get; protected set; }
        public ContaFinanceira Conta { get; protected set; } = null!; // Fix for CS8618

        public decimal Valor { get; protected set; }

        public string Descricao { get; protected set; }

        public DateTime DtVencimento { get; protected set; }
        public DateTime? DtPagamento { get; protected set; }
        

        public LancamentoFinanceiro(Guid contaId, decimal valor, string descricao)
            : base()
        {
            ContaId = contaId;
            Valor = valor;
            Descricao = descricao ?? throw new ArgumentNullException(nameof(descricao));

            DtVencimento = this.DtCriacao;
        }
    }
}
