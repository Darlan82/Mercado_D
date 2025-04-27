using MercadoD.Domain;
using MercadoD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MercadoD.Persistence.Sql.Data
{
    internal class UnitWork : IUnitWork
    {
        private DbContext _dbContext;               

        public UnitWork(MercadoEFContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public async Task CommitAsync()
        {
            //await SaveAsync();

            if (_dbContext.Database.CurrentTransaction != null)
            {
                await _dbContext.Database.CommitTransactionAsync();
            }
        }

        public void Dispose()
        {
            //_dbContext.Dispose();
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

        //public async Task<IDbTransaction> BeginTransactionAsync()
        //{
        //    return new DbTransaction(await _dbContext.Database.BeginTransactionAsync());
        //}

        //public async Task RollbackAsync()
        //{
        //    if (_dbContext.Database.CurrentTransaction != null)
        //    {
        //        await _dbContext.Database.RollbackTransactionAsync();
        //    }
        //}

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
