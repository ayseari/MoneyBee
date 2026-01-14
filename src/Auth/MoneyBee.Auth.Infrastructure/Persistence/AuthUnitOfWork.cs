namespace MoneyBee.Auth.Infrastructure.Persistence
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using MoneyBee.Auth.Domain.Interfaces;
    using MoneyBee.Auth.Infrastructure.Persistence.Contexts;

    /// <summary>
    /// unif of work for auth db context
    /// </summary>
    public class AuthUnitOfWork : IAuthUnitOfWork
    {
        private readonly IAuthDbContext _dataContext;
        private IDbContextTransaction _transaction;

        public AuthUnitOfWork(IAuthDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Save
        /// </summary>
        public int Save()
        {
            return ((DbContext)_dataContext).SaveChanges();
        }

        /// <summary>
        /// SaveAsync
        /// </summary>
        public async Task<int> SaveAsync()
        {
            return await ((DbContext)_dataContext).SaveChangesAsync();
        }

        /// <summary>
        /// BeginTransaction
        /// </summary>
        public void BeginTransaction()
        {
            _transaction = ((DbContext)_dataContext).Database.BeginTransaction();
        }

        /// <summary>
        /// Commit
        /// </summary>
        public void Commit()
        {
            _transaction?.Commit();
        }

        /// <summary>
        /// Rollback
        /// </summary>
        public void Rollback()
        {
            _transaction?.Rollback();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _transaction?.Dispose();
        }

        /// <summary>
        /// ReloadEntity
        /// </summary>
        public void ReloadEntity(object entity)
        {
            ((DbContext)_dataContext).Entry(entity).Reload();
        }

        /// <summary>
        /// Detach added entries from context and reloads entities with modified and deleted state
        /// </summary>
        public void RefreshContext()
        {
            if (_dataContext.GetDatabase.CurrentTransaction != null && _dataContext.GetDatabase.IsRelational())
            {
                var conn = _dataContext.GetDatabase.GetDbConnection();
                if (conn.State == ConnectionState.Open)
                {
                    throw new Exception("Reload is not working properly in transactional operations");
                }
            }

            ((DbContext)_dataContext).ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList().ForEach(t => t.State = EntityState.Detached);

            var changedEntryList = ((DbContext)_dataContext).ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Deleted).ToList();

            foreach (var entry in changedEntryList)
            {
                ((DbContext)_dataContext).Entry(entry.Entity).Reload();
            }
        }
    }
}