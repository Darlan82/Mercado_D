using MercadoD.Infrastructure.ValueType;
using System.ComponentModel.DataAnnotations;

namespace MercadoD.Domain.Loja.FluxoCaixa
{
    public class SaldoConsolidadoDiario : EntityBase
    {
        public DayStamp Data { get; private set; }

        public decimal SaldoTotal { get { return SaldoPrevisto + SaldoRealizado; } }

        public decimal SaldoPrevisto { get; protected set; }
        public decimal SaldoRealizado { get; protected set; }

        public Guid ContaId { get; protected set; }
        public ContaFinanceira Conta { get; protected set; } = null!; // Fix for CS8618

        [Timestamp]
        public byte[] Version { get; set; } = null!;

        #pragma warning disable CS8618 // Construtor para o EF
        private SaldoConsolidadoDiario()
        {
        }
        #pragma warning restore CS8618

        public SaldoConsolidadoDiario(DayStamp data, Guid contaId)
            : base()
        {
            Data = data;
            ContaId = contaId;
        }

        public void SomaSaldoPrevisto(decimal valor)
        {
            SaldoPrevisto += valor;
        }

        public void SomaSaldoRealizado(decimal valor)
        {
            SaldoRealizado += valor;
        }
    }
}
