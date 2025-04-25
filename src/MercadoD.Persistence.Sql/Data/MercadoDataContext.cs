using MercadoD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace MercadoD.Persistence.Sql.Data
{
    internal class MercadoDataContext : IDataContext
    {
        private MercadoEFContext _efContext;

        public MercadoDataContext(MercadoEFContext efContext)
        {
            _efContext = efContext;
        }

        public void Dispose()
        {            
        }

        

        public async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = _efContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await _efContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<TEntity?> GetByIdAsync<TEntity>(object id) where TEntity : class
        {
            return await _efContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task DeleteAsync<TEntity>(object id) where TEntity : class
        {
            TEntity? entityToDelete = await GetByIdAsync<TEntity>(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public virtual void Delete<TEntity>(TEntity entityToDelete)
            where TEntity : class
        {
            var dbSet = _efContext.Set<TEntity>();
            if (_efContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update<TEntity>(TEntity entityToUpdate)
            where TEntity : class
        {
            var dbSet = _efContext.Set<TEntity>();
            dbSet.Attach(entityToUpdate);
            _efContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class
        {
            return _efContext.Set<TEntity>();
        }
    }
}
