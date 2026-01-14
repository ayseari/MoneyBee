namespace MoneyBee.Transfer.Infrastructure.Persistence.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using MoneyBee.Transfer.Domain.Entities;

    /// <summary>
    /// Transfer db context
    /// </summary>
    public interface ITransferDbContext
    {
        DatabaseFacade GetDatabase { get; }

        /// <summary>
        /// Transactions
        /// </summary>
        DbSet<TransactionEntity> Transactions { get; set; }

        /// <summary>
        /// Transaction Status Logs
        /// </summary>
        DbSet<TransactionStatusLogEntity> TransactionStatusLogs { get; set; }
    }
}