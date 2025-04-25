using MercadoD.Infrastructure.Data;
using System.Linq.Expressions;

namespace MercadoD.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> :IRepositoryBase<TEntity>
        where TEntity : class
    {
        protected IDataContext DataContext { get; private set; }

        public RepositoryBase(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public abstract IQueryable<TEntity> GetDefaultQuery();

        public virtual async Task<TEntity?> GetByIdAsync(object id)
        {
            return await DataContext.GetByIdAsync<TEntity>(id);
        }

        protected virtual async Task<IEnumerable<TEntity>> GetAsync(
             Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            return await DataContext.GetAsync(filter, orderBy, includeProperties);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await DataContext.InsertAsync(entity);
        }

        public void Update(TEntity entity)
        {
            DataContext.Update(entity);
        }

        public async Task DeleteAsync(object id)
        {
            await DataContext.DeleteAsync<TEntity>(id);
        }

        public void Delete(TEntity entityToDelete)
        {
            DataContext.Delete(entityToDelete);
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
