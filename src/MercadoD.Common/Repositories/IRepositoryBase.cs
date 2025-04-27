namespace MercadoD.Common.Repositories
{
    public interface IRepositoryBase<TEntity> : IRepository
        where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(object id);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task DeleteAsync(object id);
        void Delete(TEntity entityToDelete);
    }
}
