namespace MoneyBee.Auth.Application.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Request model for API key validation
    /// </summary>
    public record ValidateApiKeyRequest
    {
        /// <summary>
        /// API key to validate
        /// </summary>
        /// <example>mb_xK9mP2nQ5rT8vW3yZ4aB6cD7eF9gH0jK1lM2</example>
        [Required]
        public string ApiKey { get; init; } = string.Empty;
    }
}