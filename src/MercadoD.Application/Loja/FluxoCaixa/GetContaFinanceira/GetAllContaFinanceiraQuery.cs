using MercadoD.Application.Data;

namespace MercadoD.Application.Loja.FluxoCaixa.GetContaFinanceira
{
    public record GetAllContaFinanceiraQuery(int PaginaAtual, int QtdRegistros) 
        : GetPaginatedQuery(PaginaAtual, QtdRegistros);
}
