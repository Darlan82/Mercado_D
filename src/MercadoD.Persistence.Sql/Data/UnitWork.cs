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

        public void Commit()
        {
            _dbContext.SaveChanges();

            if (_dbContext.Database.CurrentTransaction != null)
            {
                _dbContext.Database.CommitTransaction();
            }
        }

        public void Dispose()
        {
            //_dbContext.Dispose();
        }

        public void Rollback()
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                _dbContext.Database.RollbackTransaction();
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
