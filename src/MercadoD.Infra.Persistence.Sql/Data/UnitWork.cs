using MercadoD.Common.Data;
using MercadoD.Domain;
using Microsoft.EntityFrameworkCore;

namespace MercadoD.Infra.Persistence.Sql.Data
{
    internal class UnitWork : IUnitWork
    {
        private DbContext _dbContext;               

        public UnitWork(MercadoEFContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public async Task ExecuteTransactionAsync(Func<Task> operation)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    await operation.Invoke();

                    await SaveAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task SaveAsync()
        {
            foreach(var e in _dbContext.ChangeTracker.Entries<EntityBase>())
            {
                if (e.State == EntityState.Modified) 
                    e.Entity.AlterarDataAlteracao();
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
