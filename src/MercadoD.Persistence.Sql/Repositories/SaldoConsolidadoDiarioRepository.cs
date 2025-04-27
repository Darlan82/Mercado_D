using MercadoD.Domain.Loja.FluxoCaixa;
using MercadoD.Infrastructure.Data;
using MercadoD.Infrastructure.Repositories;
using MercadoD.Infrastructure.ValueType;
using Microsoft.EntityFrameworkCore;

namespace MercadoD.Persistence.Sql.Repositories
{
    internal class SaldoConsolidadoDiarioRepository : RepositoryBase<SaldoConsolidadoDiario>, ISaldoConsolidadoDiarioRepository
    {
        public SaldoConsolidadoDiarioRepository(IDataContext dataContext) 
            : base(dataContext)
        {
        }

        public async Task<SaldoConsolidadoDiario?> GetByContaIdDate(Guid contaId, DayStamp dayStamp)
        {
            return await this.GetDefaultQuery()
                .FirstOrDefaultAsync(s => s.ContaId == contaId && s.Data == dayStamp);
        }

        public override IQueryable<SaldoConsolidadoDiario> GetDefaultQuery()
        {
            return DataContext.GetQuery<SaldoConsolidadoDiario>()
                .Include(s => s.Conta);
        }
    }
}
