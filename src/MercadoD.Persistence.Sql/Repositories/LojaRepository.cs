using MercadoD.Domain.Loja;
using MercadoD.Infrastructure.Data;
using MercadoD.Infrastructure.Repositories;

namespace MercadoD.Persistence.Sql.Repositories
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
