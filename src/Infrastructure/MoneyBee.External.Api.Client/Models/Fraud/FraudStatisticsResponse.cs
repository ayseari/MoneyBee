namespace MoneyBee.External.Api.Client.Models.Fraud
{
    using MoneyBee.External.Api.Client.Enums.Fraud;
    using Newtonsoft.Json;

    /// <summary>
    /// Fraud statistics response
    /// </summary>
    public class FraudStatisticsResponse
    {
        /// <summary>
        /// Success flag
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Statistics data
        /// </summary>
        [JsonProperty("data")]
        public FraudStatisticsData Data { get; set; } = new();
    }

    /// <summary>
    /// Fraud statistics data
    /// </summary>
    public class FraudStatisticsData
    {
        /// <summary>
        /// Total transactions
        /// </summary>
        [JsonProperty("totalTransactions")]
        public int TotalTransactions { get; set; }

        /// <summary>
        /// Blocked transactions
        /// </summary>
        [JsonProperty("blockedTransactions")]
        public int BlockedTransactions { get; set; }

        /// <summary>
        /// Suspicious transactions
        /// </summary>
        [JsonProperty("suspiciousTransactions")]
        public int SuspiciousTransactions { get; set; }

        /// <summary>
        /// Average risk score
        /// </summary>
        [JsonProperty("averageRiskScore")]
        public int AverageRiskScore { get; set; }

        /// <summary>
        /// Risk level counts
        /// </summary>
        [JsonProperty("riskLevelCounts")]
        public RiskLevelCounts RiskLevelCounts { get; set; } = new();

        /// <summary>
        /// Active users
        /// </summary>
        [JsonProperty("activeUsers")]
        public int ActiveUsers { get; set; }

        /// <summary>
        /// Cached transactions
        /// </summary>
        [JsonProperty("cachedTransactions")]
        public int CachedTransactions { get; set; }
    }

    /// <summary>
    /// Risk level counts
    /// </summary>
    public class RiskLevelCounts
    {
        /// <summary>
        /// High risk count
        /// </summary>
        [JsonProperty("HIGH")]
        public int HIGH { get; set; }

        /// <summary>
        /// Low risk count
        /// </summary>
        [JsonProperty("LOW")]
        public int LOW { get; set; }
    }
}
