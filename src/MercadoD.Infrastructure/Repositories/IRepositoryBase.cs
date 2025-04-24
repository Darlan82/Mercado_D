namespace MercadoD.Infrastructure.Repositories
{
    public interface IRepositoryBase<TEntity> : IRepository
        where TEntity : class
    {
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task DeleteAsync(object id);
        void Delete(TEntity entityToDelete);
    }
}
