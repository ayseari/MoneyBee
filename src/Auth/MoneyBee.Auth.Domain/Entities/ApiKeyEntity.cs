namespace MoneyBee.Auth.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using MoneyBee.Common.DomainModels;

    /// <summary>
    /// ApiKey entity
    /// </summary>
    public class ApiKeyEntity : Entity<long>
    {
        /// <summary>
        /// SHA256 hash of the API key. 
        /// Plain text key is NEVER stored in database for security.
        /// </summary>
        [MaxLength(64)]
        public string HashedKey { get; set; } = string.Empty;

        /// <summary>
        /// Client name
        /// </summary>
        [MaxLength(200)]
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Rate Limit
        /// </summary>
        public int RateLimit { get; set; } = 100;

        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// RevokedAt
        /// </summary>
        public DateTime? RevokedAt { get; set; }

        /// <summary>
        /// LastUsedAt
        /// </summary>
        public DateTime? LastUsedAt { get; set; }

        /// <summary>
        /// CreatedBy
        /// </summary>
        [MaxLength(100)]
        public string CreatedBy { get; set; } = "system";
    }
}