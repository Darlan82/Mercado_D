using MercadoD.Application.Data;

namespace MercadoD.Application.Loja.FluxoCaixa.GetContaFinanceira
{
    public record GetAllContaFinanceiraQuery(int PaginaAtual, int QtdRegistros) 
        : GetPaginatedQuery(PaginaAtual, QtdRegistros);

    //public sealed class GetAllContaFinanceiraQuery : GetPaginatedQuery<FilterAll>
    //{
    //    public GetAllContaFinanceiraQuery(int paginaAtual, int qtdRegistros)
    //        : base(new FilterAll(), paginaAtual, qtdRegistros)
    //    {
    //    }
    //}
}
