namespace MercadoD.Application.Loja.FluxoCaixa.GetContaFinanceira
{
    public sealed class ContaFinanceiraDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } 
        public decimal SaldoPrevisto { get;  set; }
        public decimal SaldoRealizado { get; set; }

        public int TipoId { get; set; }
        public string TipoNome { get; set; }

        public Guid LojaId { get; set; }
        public string LojaNome { get; set; }

        public ContaFinanceiraDto(Guid id, string nome, decimal saldoPrevisto, decimal saldoRealizado, 
            int tipoId, string tipoNome, Guid lojaId, string lojaNome)
        {
            Id = id;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            SaldoPrevisto = saldoPrevisto;
            SaldoRealizado = saldoRealizado;
            TipoId = tipoId;
            TipoNome = tipoNome ?? throw new ArgumentNullException(nameof(tipoNome));
            LojaId = lojaId;
            LojaNome = lojaNome ?? throw new ArgumentNullException(nameof(lojaNome));
        }
    }
}
