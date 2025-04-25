namespace MercadoD.Domain.Loja.FluxoCaixa
{
    public static class FluxoCaixaConstants
    {
        public sealed class ContaFinanceira
        {
            public const int NomeMaxLength = 100;
        }

        public sealed class LancamentoFinanceiro
        {
            public const int DescricaoMaxLength = 200;
            public const int DescricaoMinLength = 3;
        }
    }
}
