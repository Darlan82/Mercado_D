using MercadoD.Common.Data;
using MercadoD.Common.Repositories;
using MercadoD.Domain.Loja.FluxoCaixa;
using Microsoft.EntityFrameworkCore;

namespace MercadoD.Infra.Persistence.Sql.Repositories
{
    internal class LancamentoFinanceiroRepository : RepositoryBase<LancamentoFinanceiro>, ILancamentoFinanceiroRepository
    {
        public LancamentoFinanceiroRepository(IDataContext dataContext) 
            : base(dataContext)
        {
        }

        public override async Task<LancamentoFinanceiro?> GetByIdAsync(object id)
        {
            var id_ = (Guid)id;
            return await GetDefaultQuery()
                .FirstOrDefaultAsync(c => c.Id == id_);
        }

        public override IQueryable<LancamentoFinanceiro> GetDefaultQuery()
        {
            return this.DataContext.GetQuery<LancamentoFinanceiro>()
                .Include(l => l.Conta);
        }
    }
}
