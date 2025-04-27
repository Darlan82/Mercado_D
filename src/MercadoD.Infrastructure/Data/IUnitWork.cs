namespace MercadoD.Infrastructure.Data
{
    public interface IUnitWork : IDisposable
    {
        //void Commit();

        //void Rollback();

        Task ExecuteTransactionAsync(Func<Task> operation);

        Task SaveAsync();
    }
}
