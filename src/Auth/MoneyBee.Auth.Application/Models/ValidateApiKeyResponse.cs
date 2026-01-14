namespace MoneyBee.Auth.Application.Models
{
    /// <summary>
    /// Response model for API key validation
    /// </summary>
    public record ValidateApiKeyResponse
    {
        /// <summary>
        /// Whether the API key is valid
        /// </summary>
        /// <example>true</example>
        public bool IsValid { get; init; }

        /// <summary>
        /// Client name (if valid)
        /// </summary>
        /// <example>Test Client</example>
        public string ClientName { get; init; }

        /// <summary>
        /// Rate limit (if valid)
        /// </summary>
        /// <example>100</example>
        public int? RateLimit { get; init; }
    }
}