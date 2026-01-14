namespace MoneyBee.Customer.Domain.Interfaces
{
    /// <summary>
    /// unif of work for customer db context
    /// </summary>
    public interface ICustomerUnitOfWork
    {
        /// <summary>
        /// Save
        /// </summary>
        int Save();

        /// <summary>
        /// SaveAsync
        /// </summary>
        Task<int> SaveAsync();

        /// <summary>
        /// BeginTransaction
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commit
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback
        /// </summary>
        void Rollback();

        /// <summary>
        /// ReloadEntity
        /// </summary>
        void ReloadEntity(object entity);

        /// <summary>
        /// Detach added entries from context and reloads entities with modified and deleted state
        /// </summary>
        void RefreshContext();
    }
}