using MercadoD.Common.Repositories;

namespace MercadoD.Domain.Loja.FluxoCaixa
{
    public interface IContaFinanceiraRepository : IRepositoryBase<ContaFinanceira>
    {
        Task<PagedResult<ContaFinanceira>> GetAllAsync(int paginaAtual, int qtdRegistros);
    }
}
