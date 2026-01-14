namespace MoneyBee.Auth.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using MoneyBee.Auth.Domain.Entities;
    using MoneyBee.Auth.Domain.Interfaces;
    using MoneyBee.Auth.Domain.Repositories;
    using MoneyBee.Auth.Infrastructure.Persistence.Contexts;

    /// <summary>
    /// Auth Repository
    /// </summary>
    /// <param name="authDbContext"></param>
    public class ApiKeyRepository(IAuthDbContext authDbContext, IAuthUnitOfWork authUnitOfWork) : IApiKeyRepository
    {
        private readonly AuthDbContext _authDbContext = (AuthDbContext)authDbContext;
        private readonly DbSet<ApiKeyEntity> _dbSet = authDbContext.ApiKeys;

        /// <summary>
        /// Get by id
        /// </summary>
        public async Task<ApiKeyEntity?> GetByIdAsync(long id)
            => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        /// <summary>
        /// Add Active Apikey async
        /// </summary>
        public async Task<ApiKeyEntity?> GetActiveApiKeyAsync(string hashedKey)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.HashedKey == hashedKey && x.IsActive);
        }

        /// <summary>
        /// Get all API keys
        /// </summary>
        public async Task<List<ApiKeyEntity>> GetAllAsync()
        {
            return await _dbSet.Where(x =>x.IsActive).ToListAsync();
        }

        /// <summary>
        /// Add Apikey async
        /// </summary>
        public async Task AddAsync(ApiKeyEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Update API key
        /// </summary>
        public async Task UpdateAsync(ApiKeyEntity entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Save
        /// </summary>
        public async Task SaveAsync()
        {
            await authUnitOfWork.SaveAsync();
        }
    }
}