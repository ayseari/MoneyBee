namespace MoneyBee.Auth.Infrastructure.Persistence.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using MoneyBee.Auth.Domain.Entities;
    using MoneyBee.Auth.Infrastructure.Persistence.Configurations;

    /// <summary>
    /// Auth db context
    /// </summary>
    public class AuthDbContext : DbContext, IAuthDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DatabaseFacade GetDatabase => Database;

        public DbSet<ApiKeyEntity> ApiKeys { get; set; }

        /// <summary>
        /// On model creating
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApiKeyEntityConfiguration());
        }
    }
}