namespace MoneyBee.Customer.Infrastructure.Persistence.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using MoneyBee.Customer.Domain.Entities;

    /// <summary>
    /// Auth db context
    /// </summary>
    public interface ICustomerDbContext
    {
        DatabaseFacade GetDatabase { get; }

        /// <summary>
        /// ApiKeys
        /// </summary>
        DbSet<CustomerEntity> Customers { get; set; }
    }
}