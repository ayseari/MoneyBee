namespace MoneyBee.Auth.Application.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Update Rate Limit Request
    /// </summary>
    public class UpdateRateLimitRequest
    {
        /// <summary>
        /// Rate Limit (requests per minute)
        /// </summary>
        /// <example>100</example>
        [Range(1, int.MaxValue, ErrorMessage = "Rate limit must be greater than 0")]
        public int RateLimit { get; set; }
    }
}