namespace MoneyBee.Auth.Application.Services.Interfaces
{
    using MoneyBee.Auth.Application.Models;
    using MoneyBee.Common.Models.Result;

    /// <summary>
    /// Api Key Service
    /// </summary>
    public interface IApiKeyService
    {
        /// <summary>
        /// Generate Api Key Async
        /// </summary>
        Task<ServiceResult<ApiKeyResponse>> GenerateApiKeyAsync(CreateApiKeyRequest request);

        /// <summary>
        /// Validate Api Key Async
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        Task<ServiceResult<bool>> ValidateApiKeyAsync(string apiKey);

        /// <summary>
        /// Get all API keys
        /// </summary>
        Task<ServiceResult<List<ApiKeyDetailResponse>>> GetAllApiKeysAsync();

        /// <summary>
        /// Get API key by ID
        /// </summary>
        Task<ServiceResult<ApiKeyDetailResponse>> GetApiKeyByIdAsync(long id);

        /// <summary>
        /// Update API key
        /// </summary>
        Task<ServiceResult<ApiKeyDetailResponse>> UpdateApiKeyAsync(long id, UpdateApiKeyRequest request);

        /// <summary>
        /// Revoke API key
        /// </summary>
        Task<ServiceResult> RevokeApiKeyAsync(long id);

        /// <summary>
        /// Update rate limit
        /// </summary>
        Task<ServiceResult<ApiKeyDetailResponse>> UpdateRateLimitAsync(long id, UpdateRateLimitRequest request);
    }
}