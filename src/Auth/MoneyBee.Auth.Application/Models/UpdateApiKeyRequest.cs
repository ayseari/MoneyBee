namespace MoneyBee.Auth.Application.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Update API Key Request
    /// </summary>
    public class UpdateApiKeyRequest
    {
        /// <summary>
        /// Client name
        /// </summary>
        /// <example>Test Client</example>
        [MaxLength(200)]
        public string ClientName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        /// <example>API key for testing</example>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Rate Limit
        /// </summary>
        /// <example>100</example>
        public int? RateLimit { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        /// <example>true</example>
        public bool? IsActive { get; set; }
    }
}