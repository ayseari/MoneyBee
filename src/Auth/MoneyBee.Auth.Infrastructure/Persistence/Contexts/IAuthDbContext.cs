namespace MoneyBee.Auth.Infrastructure.Persistence.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using MoneyBee.Auth.Domain.Entities;

    /// <summary>
    /// Auth db context
    /// </summary>
    public interface IAuthDbContext
    {
        DatabaseFacade GetDatabase { get; }

        /// <summary>
        /// ApiKeys
        /// </summary>
        DbSet<ApiKeyEntity> ApiKeys { get; set; }
    }
}