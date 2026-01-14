namespace MoneyBee.Customer.Infrastructure.Persistence.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using MoneyBee.Customer.Domain.Entities;
    using MoneyBee.Customer.Infrastructure.Persistence.Configurations;

    /// <summary>
    /// Customer db context
    /// </summary>
    public class CustomerDbContext : DbContext, ICustomerDbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        public DatabaseFacade GetDatabase => Database;

        public DbSet<CustomerEntity> Customers { get; set; }

        /// <summary>
        /// On model creating
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
        }
    }
}