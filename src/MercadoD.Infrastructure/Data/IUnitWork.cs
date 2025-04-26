namespace MercadoD.Infrastructure.Data
{
    public interface IUnitWork : IDisposable
    {
        //void Commit();

        //void Rollback();

        Task<IDbTransaction> BeginTransactionAsync();

        Task SaveAsync();
    }
}
