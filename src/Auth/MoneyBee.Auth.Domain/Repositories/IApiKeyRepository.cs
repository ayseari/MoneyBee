namespace MoneyBee.Auth.Domain.Repositories
{
    using MoneyBee.Auth.Domain.Entities;

    /// <summary>
    /// Api key repository
    /// </summary>
    public interface IApiKeyRepository
    {
        /// <summary>
        /// Get by id
        /// </summary>
        Task<ApiKeyEntity?> GetByIdAsync(long id);

        /// <summary>
        /// Add Active Apikey async
        /// </summary>
        Task<ApiKeyEntity?> GetActiveApiKeyAsync(string hashedKey);

        /// <summary>
        /// Get all API keys
        /// </summary>
        Task<List<ApiKeyEntity>> GetAllAsync();

        /// <summary>
        /// Add Apikey async
        /// </summary>
        Task AddAsync(ApiKeyEntity entity);

        /// <summary>
        /// Update API key
        /// </summary>
        Task UpdateAsync(ApiKeyEntity entity);

        /// <summary>
        /// Save
        /// </summary>
        Task SaveAsync();
    }
}
