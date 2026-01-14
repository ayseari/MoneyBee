namespace MoneyBee.Transfer.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using MoneyBee.Transfer.Domain.Entities;
    using MoneyBee.Transfer.Domain.Enums;
    using MoneyBee.Transfer.Domain.Interfaces;
    using MoneyBee.Transfer.Domain.Repositories;
    using MoneyBee.Transfer.Infrastructure.Persistence.Contexts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Transaction Repository
    /// </summary>
    public class TransactionRepository(ITransferDbContext customerDbContext, ITransferUnitOfWork customerUnitOfWork) : ITransactionRepository
    {
        private readonly TransferDbContext _customerDbContext = (TransferDbContext)customerDbContext;
        private readonly DbSet<TransactionEntity> _dbSet = customerDbContext.Transactions;

        /// <summary>
        /// Get by id
        /// </summary>
        public async Task<TransactionEntity?> GetById(Guid id) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<TransactionEntity?> GetByTransactionCodeAsync(string transactionCode)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.TransactionCode == transactionCode);
        }

        /// <summary>
        /// AnyByTransactionCode
        /// </summary>
        public async Task<bool> AnyByTransactionCode(string transactionCode)
        {
            return await _dbSet.AnyAsync(t => t.TransactionCode == transactionCode);
        }

        public async Task<List<TransactionEntity>> GetByCustomerIdAsync(Guid customerId, DateTime date)
        {
            return await _dbSet
                .Where(t => t.SenderId == customerId
                    && t.CreatedAt.Date == date.Date)
                .ToListAsync();
        }

        public async Task<decimal> GetDailyTotalByCustomerIdAsync(Guid customerId)
        {
            var date = DateTime.UtcNow;

            return await _dbSet
                .Where(t => t.SenderId == customerId
                    && t.CreatedAt.Date == date.Date
                    && (t.Status == TransactionStatus.APPROVED
                        || t.Status == TransactionStatus.COMPLETED
                        || t.Status == TransactionStatus.WAITING_APPROVAL))
                .SumAsync(t => t.AmountInTRY);
        }

        /// <summary>
        /// Add Apikey async
        /// </summary>
        public async Task AddAsync(TransactionEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Update API key
        /// </summary>
        public async Task UpdateAsync(TransactionEntity entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Save
        /// </summary>
        public async Task SaveAsync()
        {
            await customerUnitOfWork.SaveAsync();
        }
    }
}