namespace MoneyBee.Auth.Application.Services
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using MoneyBee.Auth.Application.Models;
    using MoneyBee.Auth.Application.Services.Interfaces;
    using MoneyBee.Auth.Domain.Entities;
    using MoneyBee.Auth.Domain.Repositories;
    using MoneyBee.Common.Models.Result;

    /// <summary>
    /// Api Key Service
    /// </summary>
    public class ApiKeyService(IApiKeyRepository apiKeyRepository) : IApiKeyService
    {
        /// <summary>
        /// Generate Api Key Async
        /// </summary>
        public async Task<ServiceResult<ApiKeyResponse>> GenerateApiKeyAsync(CreateApiKeyRequest request)
        {
            var apiKey = GenerateSecureApiKey();

            var hashedKey = HashApiKey(apiKey);

            var entity = new ApiKeyEntity
            {
                HashedKey = hashedKey,
                ClientName = request.ClientName,
                Description = request.Description,
                RateLimit = request.RateLimit ?? 100,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy               
            };

            await apiKeyRepository.AddAsync(entity);
            await apiKeyRepository.SaveAsync();

            var response = new ApiKeyResponse
            {
                Id = entity.Id,
                ApiKey = apiKey,
                ClientName = entity.ClientName,
                RateLimit = entity.RateLimit,
                Message = "⚠️ Save this API key securely. It won't be shown again!"
            };

            return ServiceResult.Success(response);
        }

        /// <summary>
        /// Validate Api Key Async
        /// </summary>
        public async Task<ServiceResult<bool>> ValidateApiKeyAsync(string apiKey)
        {
            var hashedKey = HashApiKey(apiKey);

            var entity = await apiKeyRepository.GetActiveApiKeyAsync(hashedKey);

            if (entity != null)
            {
                //TODO : entity'e taşı
                entity.LastUsedAt = DateTime.UtcNow;
                await apiKeyRepository.SaveAsync();
                return ServiceResult.Success(true);
            }

            return ServiceResult.Failure<bool>("Invalid api key");
        }

        /// <summary>
        /// Get all API keys
        /// </summary>
        public async Task<ServiceResult<List<ApiKeyDetailResponse>>> GetAllApiKeysAsync()
        {
            var entities = await apiKeyRepository.GetAllAsync();

            var response = entities.Select(e => new ApiKeyDetailResponse
            {
                Id = e.Id,
                ClientName = e.ClientName,
                Description = e.Description,
                RateLimit = e.RateLimit,
                IsActive = e.IsActive,
                CreatedAt = e.CreatedAt,
                RevokedAt = e.RevokedAt,
                LastUsedAt = e.LastUsedAt,
                CreatedBy = e.CreatedBy
            }).ToList();

            return ServiceResult.Success(response);
        }

        /// <summary>
        /// Get API key by ID
        /// </summary>
        public async Task<ServiceResult<ApiKeyDetailResponse>> GetApiKeyByIdAsync(long id)
        {
            var entity = await apiKeyRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return ServiceResult.Failure<ApiKeyDetailResponse>("API key not found");
            }

            var response = new ApiKeyDetailResponse
            {
                Id = entity.Id,
                ClientName = entity.ClientName,
                Description = entity.Description,
                RateLimit = entity.RateLimit,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                RevokedAt = entity.RevokedAt,
                LastUsedAt = entity.LastUsedAt,
                CreatedBy = entity.CreatedBy
            };

            return ServiceResult.Success(response);
        }

        /// <summary>
        /// Update API key
        /// </summary>
        public async Task<ServiceResult<ApiKeyDetailResponse>> UpdateApiKeyAsync(long id, UpdateApiKeyRequest request)
        {
            var entity = await apiKeyRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return ServiceResult.Failure<ApiKeyDetailResponse>("API key not found");
            }

            if (request.ClientName != null)
                entity.ClientName = request.ClientName;

            if (request.Description != null)
                entity.Description = request.Description;

            if (request.RateLimit.HasValue)
                entity.RateLimit = request.RateLimit.Value;

            if (request.IsActive.HasValue)
            {
                entity.IsActive = request.IsActive.Value;
                if (!request.IsActive.Value && entity.RevokedAt == null)
                {
                    entity.RevokedAt = DateTime.UtcNow;
                }
                else if (request.IsActive.Value)
                {
                    entity.RevokedAt = null;
                }
            }

            await apiKeyRepository.UpdateAsync(entity);
            await apiKeyRepository.SaveAsync();

            var response = new ApiKeyDetailResponse
            {
                Id = entity.Id,
                ClientName = entity.ClientName,
                Description = entity.Description,
                RateLimit = entity.RateLimit,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                RevokedAt = entity.RevokedAt,
                LastUsedAt = entity.LastUsedAt,
                CreatedBy = entity.CreatedBy
            };

            return ServiceResult.Success(response);
        }

        /// <summary>
        /// Revoke API key
        /// </summary>
        public async Task<ServiceResult> RevokeApiKeyAsync(long id)
        {
            var entity = await apiKeyRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return ServiceResult.Failure("API key not found");
            }

            entity.IsActive = false;
            entity.RevokedAt = DateTime.UtcNow;

            await apiKeyRepository.UpdateAsync(entity);
            await apiKeyRepository.SaveAsync();

            return ServiceResult.Success();
        }

        /// <summary>
        /// Update rate limit
        /// </summary>
        public async Task<ServiceResult<ApiKeyDetailResponse>> UpdateRateLimitAsync(long id, UpdateRateLimitRequest request)
        {
            var entity = await apiKeyRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return ServiceResult.Failure<ApiKeyDetailResponse>("API key not found");
            }

            entity.RateLimit = request.RateLimit;

            await apiKeyRepository.UpdateAsync(entity);
            await apiKeyRepository.SaveAsync();

            var response = new ApiKeyDetailResponse
            {
                Id = entity.Id,
                ClientName = entity.ClientName,
                Description = entity.Description,
                RateLimit = entity.RateLimit,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                RevokedAt = entity.RevokedAt,
                LastUsedAt = entity.LastUsedAt,
                CreatedBy = entity.CreatedBy
            };

            return ServiceResult.Success(response);
        }

        #region Private Methods
        /// <summary>
        /// Generate a new API key
        /// </summary>
        private static string GenerateSecureApiKey()
        {
            const string prefix = "mb_";
            var bytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            var base64Key = Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
            return $"{prefix}{base64Key}";
        }

        /// <summary>
        /// Hash API key using SHA256
        /// </summary>
        private static string HashApiKey(string apiKey)
        {
            var bytes = Encoding.UTF8.GetBytes(apiKey);
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLowerInvariant();
        }

        //private string GenerateSecureApiKey()
        //{
        //    // Cryptographically secure random
        //    var bytes = new byte[32];
        //    using var rng = RandomNumberGenerator.Create();
        //    rng.GetBytes(bytes);

        //    return $"mb_{Convert.ToBase64String(bytes).Replace("+", "").Replace("/", "").Substring(0, 40)}";
        //    // Örnek: mb_xK9mP2nQ5rT8vW3yZ4aB6cD7eF9gH0jK1lM2
        //}

        //private string HashApiKey(string apiKey)
        //{
        //    using var sha256 = SHA256.Create();
        //    var bytes = Encoding.UTF8.GetBytes(apiKey);
        //    var hash = sha256.ComputeHash(bytes);
        //    return Convert.ToBase64String(hash);
        //}
        #endregion
    }
}