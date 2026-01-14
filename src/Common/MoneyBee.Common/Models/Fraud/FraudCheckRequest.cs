namespace MoneyBee.Common.Models.Fraud
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Fraud check request
    /// </summary>
    public class FraudCheckRequest
    {
        /// <summary>
        /// Transaction ID
        /// </summary>
        [Required]
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; } = string.Empty;

        /// <summary>
        /// User ID
        /// </summary>
        [Required]
        [JsonProperty("userId")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// To User ID
        /// </summary>
        [JsonProperty("toUserId")]
        public string? ToUserId { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [Required]
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [Required]
        [JsonProperty("currency")]
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// Metadata
        /// </summary>
        [JsonProperty("metadata")]
        public Dictionary<string, object>? Metadata { get; set; }
    }
}
