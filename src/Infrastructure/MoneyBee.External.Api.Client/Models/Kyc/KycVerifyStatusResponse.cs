namespace MoneyBee.External.Api.Client.Models.Kyc
{
    using System;
    using MoneyBee.External.Api.Client.Enums.Kyc;
    using Newtonsoft.Json;

    /// <summary>
    /// KYC Verification Status Response (200 - Success)
    /// </summary>
    public class KycVerifyStatusResponse
    {
        /// <summary>
        /// Success flag
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Unique verification ID
        /// </summary>
        [JsonProperty("verificationId")]
        public Guid VerificationId { get; set; }

        /// <summary>
        /// Verification result
        /// </summary>
        [JsonProperty("verified")]
        public bool Verified { get; set; }

        /// <summary>
        /// Verification score (0-100)
        /// </summary>
        [JsonProperty("score")]
        public int Score { get; set; }

        /// <summary>
        /// Verification level
        /// </summary>
        [JsonProperty("level")]
        public KycVerificationLevel Level { get; set; }

        /// <summary>
        /// Verification timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
