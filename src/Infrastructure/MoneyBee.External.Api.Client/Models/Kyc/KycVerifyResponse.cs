namespace MoneyBee.External.Api.Client.Models.Kyc
{
    using MoneyBee.External.Api.Client.Enums.Kyc;
    using Newtonsoft.Json;

    /// <summary>
    /// KYC Verification Response
    /// </summary>
    public class KycVerifyResponse
    {
        /// <summary>
        /// Verification success flag
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Unique verification ID
        /// </summary>
        [JsonProperty("verificationId")]
        public Guid VerificationId { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Verification result
        /// </summary>
        [JsonProperty("verified")]
        public bool Verified { get; set; }

        /// <summary>
        /// Verification reason
        /// </summary>
        [JsonProperty("reason")]
        public KycVerificationReason Reason { get; set; }

        /// <summary>
        /// Verification score (0-100)
        /// </summary>
        [JsonProperty("verificationScore")]
        public int VerificationScore { get; set; }

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

        /// <summary>
        /// Processing time in milliseconds
        /// </summary>
        [JsonProperty("processingTimeMs")]
        public int ProcessingTimeMs { get; set; }
    }
}