namespace MoneyBee.Transfer.Infrastructure.Persistence.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using MoneyBee.Transfer.Domain.Entities;
    using MoneyBee.Transfer.Infrastructure.Persistence.Configurations;

    /// <summary>
    /// Transfer db context
    /// </summary>
    public class TransferDbContext : DbContext, ITransferDbContext
    {
        public TransferDbContext(DbContextOptions<TransferDbContext> options) : base(options)
        {
        }

        public DatabaseFacade GetDatabase => Database;

        public DbSet<TransactionEntity> Transactions { get; set; }

        public DbSet<TransactionStatusLogEntity> TransactionStatusLogs { get; set; }

        /// <summary>
        /// On model creating
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionStatusLogEntityConfiguration());
        }
    }
}