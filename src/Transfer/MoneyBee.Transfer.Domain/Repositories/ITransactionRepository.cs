namespace MoneyBee.Transfer.Domain.Repositories
{
    using MoneyBee.Transfer.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Transaction Repository
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Get by id
        /// </summary>
        Task<TransactionEntity?> GetById(Guid id);

        Task<TransactionEntity?> GetByTransactionCodeAsync(string transactionCode);

        /// <summary>
        /// AnyByTransactionCode
        /// </summary>
        Task<bool> AnyByTransactionCode(string transactionCode);

        Task<List<TransactionEntity>> GetByCustomerIdAsync(Guid customerId, DateTime date);

        Task<decimal> GetDailyTotalByCustomerIdAsync(Guid customerId);

        /// <summary>
        /// Add Apikey async
        /// </summary>
        Task AddAsync(TransactionEntity entity);

        /// <summary>
        /// Update API key
        /// </summary>
        Task UpdateAsync(TransactionEntity entity);

        /// <summary>
        /// Save
        /// </summary>
        Task SaveAsync();
    }
}
