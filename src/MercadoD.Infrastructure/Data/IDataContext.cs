using System.Linq.Expressions;

namespace MercadoD.Infrastructure.Data
{
    public interface IDataContext : IDisposable
    {
        public IQueryable<TEntity> GetQuery<TEntity>()
            where TEntity : class;

        public Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "") where TEntity : class;

        public Task InsertAsync<TEntity>(TEntity entity) where TEntity : class;

        public Task<TEntity?> GetByIdAsync<TEntity>(object id) where TEntity : class;

        public Task DeleteAsync<TEntity>(object id) where TEntity : class;

        public void Delete<TEntity>(TEntity entityToDelete) where TEntity : class;

        public void Update<TEntity>(TEntity entity) where TEntity : class;
               

        //IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class;
        //IUnitWork CreateUnitOfWork(TransactionScope scope = TransactionScope.Required);
        //TRepository GetRepository<TRepository>() where TRepository : IRepository;
    }
}
