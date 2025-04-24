using MercadoD.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MercadoD.Domain.Loja.FluxoCaixa
{
    public class ContaFinanceira : EntityPadrao
    {
        public string Nome { get; protected set; } = string.Empty;
        public decimal SaldoPrevisto { get; protected set; }

        public ContaFinanceiraTipo Tipo { get; protected set; }

        public Guid LojaId { get; protected set; }
        public Loja Loja { get; protected set; } = null!;

        [Timestamp]
        public byte[] Version { get; set; } = null!;

        public ContaFinanceira(Guid lojaId, string nome, decimal saldoPrevisto,
            ContaFinanceiraTipo tipo)
            : base()
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            SaldoPrevisto = saldoPrevisto;
            LojaId = lojaId;
            Tipo = tipo;
        }
    }
}
