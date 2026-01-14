namespace MoneyBee.Auth.Application.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Create Api Key Request
    /// </summary>
    public class CreateApiKeyRequest
    {
        /// <summary>
        /// Client name
        /// </summary>
        /// <example>Test Client</example>
        [MaxLength(200)]
        [Required(ErrorMessage = "Client name is required")]
        public string ClientName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        /// <example>API key for development and testing purposes</example>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Rate Limit
        /// </summary>
        /// <example>100</example>
        [Range(1, 10000)]
        [DefaultValue(100)]
        public int? RateLimit { get; set; }

        /// <summary>
        /// CreatedBy
        /// </summary>
        /// <example>system</example>
        [MaxLength(100)]
        public string CreatedBy { get; set; }
    }
}
