namespace MoneyBee.Auth.Application.Models
{
    using System.ComponentModel.DataAnnotations;

    //TODO: apilere camelCase response global uygula
    /// <summary>
    /// Api Key Response
    /// </summary>
    public class ApiKeyResponse
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Api key
        /// </summary>
        /// <example>mb_xK9mP2nQ5rT8vW3yZ4aB6cD7eF9gH0jK1lM2</example>
        public string ApiKey { get; set; }

        /// <summary>
        /// Client name
        /// </summary>
        /// <example>Test Client</example>
        [MaxLength(200)]
        public string ClientName { get; set; }

        /// <summary>
        /// Rate limit (requests per minute)
        /// </summary>
        /// <example>100</example>
        public int RateLimit { get; init; }

        /// <summary>
        /// Message
        /// </summary>
        /// <example>⚠️ IMPORTANT: Save this API key securely. It will not be shown again!</example>
        [MaxLength(250)]
        public string Message { get; set; }
    }
}
