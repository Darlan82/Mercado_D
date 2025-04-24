namespace MercadoD.Infrastructure.Data
{
    public interface IUnitWork : IDisposable
    {
        //void Commit();

        //void Rollback();

        void Save();
    }
}
