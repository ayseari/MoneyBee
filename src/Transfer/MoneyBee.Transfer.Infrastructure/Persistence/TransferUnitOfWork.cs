namespace MoneyBee.Transfer.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using MoneyBee.Transfer.Domain.Interfaces;
    using MoneyBee.Transfer.Infrastructure.Persistence.Contexts;
    using System.Data;

    /// <summary>
    /// unif of work for transfer db context
    /// </summary>
    public class TransferUnitOfWork : ITransferUnitOfWork
    {
        private readonly ITransferDbContext _dataContext;
        private IDbContextTransaction _transaction;

        public TransferUnitOfWork(ITransferDbContext dataContext)
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
