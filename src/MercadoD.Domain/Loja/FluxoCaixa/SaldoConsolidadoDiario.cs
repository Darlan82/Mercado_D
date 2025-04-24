using MercadoD.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MercadoD.Domain.Loja.FluxoCaixa
{
    public class SaldoConsolidadoDiario : EntityPadrao
    {
        public DateTime Data { get; protected set; }

        public decimal SaldoTotal { get { return SaldoPrevisto + SaldoRealizado; } }

        public decimal SaldoPrevisto { get; protected set; }
        public decimal SaldoRealizado { get; protected set; }

        public Guid ContaId { get; protected set; }
        public ContaFinanceira Conta { get; protected set; } = null!; // Fix for CS8618

        [Timestamp]
        public byte[] Version { get; set; } = null!;

        public SaldoConsolidadoDiario(DateTime data, decimal saldoPrevisto, decimal saldoRealizado, Guid contaId)
        {
            Data = data;            
            SaldoPrevisto = saldoPrevisto;
            SaldoRealizado = saldoRealizado;
            ContaId = contaId;
        }
    }
}
