namespace MercadoD.Infrastructure.Data
{
    public interface IDbTransaction : IDisposable
    {
        Task CommitAsync();
    }
}
