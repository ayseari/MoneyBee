namespace MoneyBee.External.Api.Client.Models.Kyc
{
    using Newtonsoft.Json;

    /// <summary>
    /// KYC Verification Statistics Response
    /// </summary>
    public class KycVerifyStatisticsResponse
    {
        /// <summary>
        /// Success flag
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Statistics data
        /// </summary>
        [JsonProperty("statistics")]
        public KycVerifyStatisticsData Statistics { get; set; } = new();

        /// <summary>
        /// Response timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }

    /// <summary>
    /// KYC Verification Statistics Data
    /// </summary>
    public class KycVerifyStatisticsData
    {
        /// <summary>
        /// Total cached verifications
        /// </summary>
        [JsonProperty("totalCached")]
        public int TotalCached { get; set; }

        /// <summary>
        /// Count by verification levels
        /// </summary>
        [JsonProperty("levels")]
        public KycVerifyLevelCounts Levels { get; set; } = new();

        /// <summary>
        /// Count of verified users
        /// </summary>
        [JsonProperty("verifiedCount")]
        public int VerifiedCount { get; set; }

        /// <summary>
        /// Count of unverified users
        /// </summary>
        [JsonProperty("unverifiedCount")]
        public int UnverifiedCount { get; set; }
    }

    /// <summary>
    /// KYC Verification Level Counts
    /// </summary>
    public class KycVerifyLevelCounts
    {
        /// <summary>
        /// Full verification count
        /// </summary>
        [JsonProperty("FULL")]
        public int FULL { get; set; }

        /// <summary>
        /// Basic verification count
        /// </summary>
        [JsonProperty("BASIC")]
        public int BASIC { get; set; }

        /// <summary>
        /// Restricted verification count
        /// </summary>
        [JsonProperty("RESTRICTED")]
        public int RESTRICTED { get; set; }

        /// <summary>
        /// None verification count
        /// </summary>
        [JsonProperty("NONE")]
        public int NONE { get; set; }
    }
}
