using MercadoD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace MercadoD.Persistence.Sql.Data
{
    internal class DbTransaction : IDbTransaction
    {
        IDbContextTransaction _dbContextTransaction;

        public DbTransaction(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction ?? throw new ArgumentNullException(nameof(dbContextTransaction));
        }

        public async Task CommitAsync()
        {
            await _dbContextTransaction.CommitAsync();
        }

        public void Dispose()
        {
            _dbContextTransaction.Rollback();
            _dbContextTransaction.Dispose();
        }
    }
}
