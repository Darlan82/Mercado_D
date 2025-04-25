using MercadoD.Infrastructure.Time;
using System.ComponentModel.DataAnnotations;

namespace MercadoD.Domain.Loja.FluxoCaixa
{
    public class ContaFinanceira : EntityBase
    {
        public string Nome { get; protected set; } = string.Empty;
        public decimal SaldoPrevisto { get; protected set; }
        public decimal SaldoRealizado { get; protected set; }

        public ContaFinanceiraTipo Tipo { get; protected set; }

        public Guid LojaId { get; protected set; }
        public Loja Loja { get; protected set; } = null!;

        [Timestamp]
        public byte[] Version { get; set; } = null!;

        public ContaFinanceira(Guid lojaId, string nome, ContaFinanceiraTipo tipo)
            : base()
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));            
            LojaId = lojaId;
            Tipo = tipo;
        }

        public void ContabilizarSaldo(LancamentoFinanceiro lancamento)
        {
            if (!lancamento.SaldoPrevistoContabilizado)
            {
                lancamento.SaldoPrevistoContabilizado = true;
                this.SaldoPrevisto += lancamento.Valor;
            }

            if (!lancamento.SaldoRealizadoContabilizado && 
                (lancamento.DtPagamento.HasValue && lancamento.DtPagamento.Value <= Clock.UtcNow))
            {
                lancamento.SaldoRealizadoContabilizado = true;
                this.SaldoRealizado += lancamento.Valor;
            }
        }
    }
}
