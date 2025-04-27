using MercadoD.Common.Data;
using MercadoD.Common.Repositories;
using MercadoD.Domain.Loja;
using MercadoD.Domain.Loja.FluxoCaixa;
using Microsoft.EntityFrameworkCore;

namespace MercadoD.Infra.Persistence.Sql.Repositories
{
    internal class ContaFinanceiraRepository : RepositoryBase<ContaFinanceira>, IContaFinanceiraRepository
    {
        public ContaFinanceiraRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<ContaFinanceira?> GetByIdAsync(object id)
        {
            var id_ = (Guid)id;
            return await GetDefaultQuery()
                .FirstOrDefaultAsync(c => c.Id == id_);
        }

        public async Task<PagedResult<ContaFinanceira>> GetAllAsync(int paginaAtual, int qtdRegistros)
        {
            var skip = paginaAtual * qtdRegistros;

            var query = GetDefaultQuery();

            var lsContas = await query.Skip(skip).Take(qtdRegistros)                
                .ToListAsync();

            var total = await query.CountAsync();

            return new PagedResult<ContaFinanceira>(paginaAtual, qtdRegistros, total, lsContas);
        }

        public override IQueryable<ContaFinanceira> GetDefaultQuery()
        {
            return this.DataContext.GetQuery<ContaFinanceira>()
                .Include(c => c.Loja);
        }
    }
}
