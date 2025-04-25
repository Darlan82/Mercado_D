namespace MercadoD.Application.Loja.FluxoCaixa.GetLancamentoFinanceiro
{
    public sealed class LancamentoFinanceiroDto
    {
        public Guid Id { get; set; }

        public Guid ContaId { get; set; }
        public string NomeConta { get; set; } = string.Empty;

        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        
        public DateTime DtLancamento { get; set; }
        public DateTime? DtVencimento { get; set; }

        public LancamentoFinanceiroDto(Guid id, Guid contaId, string nomeConta, decimal valor, string descricao, 
            DateTime dtLancamento, DateTime? dtVencimento)
        {
            Id = id;
            ContaId = contaId;
            NomeConta = nomeConta;
            Valor = valor;
            Descricao = descricao;
            DtLancamento = dtLancamento;
            DtVencimento = dtVencimento;
        }
    }    
}
