namespace MercadoD.Common.Data
{
    public interface IUnitWork
    {
        Task ExecuteTransactionAsync(Func<Task> operation);

        Task SaveAsync();
    }
}
