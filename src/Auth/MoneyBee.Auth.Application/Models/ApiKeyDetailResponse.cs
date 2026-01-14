namespace MoneyBee.Auth.Application.Models
{
    using System;

    /// <summary>
    /// API Key Detail Response (without plain text key)
    /// </summary>
    public class ApiKeyDetailResponse
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <example>1</example>
        public long Id { get; set; }

        /// <summary>
        /// Client name
        /// </summary>
        /// <example>Test Client</example>
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        /// <example>API key for testing</example>
        public string Description { get; set; }

        /// <summary>
        /// Rate Limit
        /// </summary>
        /// <example>200</example>
        public int RateLimit { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        /// <example>true</example>
        public bool IsActive { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        /// <example>2025-01-11T10:30:00Z</example>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Revoked At
        /// </summary>
        /// <example>null</example>
        public DateTime? RevokedAt { get; set; }

        /// <summary>
        /// Last Used At
        /// </summary>
        /// <example>2025-01-11T12:45:00Z</example>
        public DateTime? LastUsedAt { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        /// <example>admin</example>
        public string CreatedBy { get; set; } = string.Empty;
    }
}