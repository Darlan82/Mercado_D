using MercadoD.Common.Data;
using MercadoD.Common.Repositories;
using MercadoD.Domain.Loja;

namespace MercadoD.Infra.Persistence.Sql.Repositories
{
    internal class LojaRepository : RepositoryBase<Loja>, ILojaRepository
    {
        public LojaRepository(IDataContext dataContext) 
            : base(dataContext)
        {
        }

        public override IQueryable<Loja> GetDefaultQuery()
        {
            return DataContext.GetQuery<Loja>();
        }
    }
}
