namespace MoneyBee.External.Api.Client.Models.Fraud
{
    using MoneyBee.Common.Enums;
    using MoneyBee.External.Api.Client.Enums.Fraud;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Fraud check response (200 - Success)
    /// </summary>
    public class FraudCheckResponse
    {
        /// <summary>
        /// Success flag
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Fraud check data
        /// </summary>
        [JsonProperty("data")]
        public FraudCheckData Data { get; set; } = new();
    }

    /// <summary>
    /// Fraud check data
    /// </summary>
    public class FraudCheckData
    {
        /// <summary>
        /// Transaction ID
        /// </summary>
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; } = string.Empty;

        /// <summary>
        /// Risk level
        /// </summary>
        [JsonProperty("riskLevel")]
        public RiskLevel RiskLevel { get; set; }

        /// <summary>
        /// Risk score
        /// </summary>
        [JsonProperty("riskScore")]
        public int RiskScore { get; set; }

        /// <summary>
        /// Risk factors
        /// </summary>
        [JsonProperty("riskFactors")]
        public List<RiskFactors> RiskFactors { get; set; } = new();

        /// <summary>
        /// Should block
        /// </summary>
        [JsonProperty("shouldBlock")]
        public bool ShouldBlock { get; set; }

        /// <summary>
        /// Recommendations
        /// </summary>
        [JsonProperty("recommendations")]
        public List<Recommendations> Recommendations { get; set; } = new();

        /// <summary>
        /// Required actions
        /// </summary>
        [JsonProperty("requiredActions")]
        public List<RequiredActions> RequiredActions { get; set; } = new();

        /// <summary>
        /// Processing time
        /// </summary>
        [JsonProperty("processingTime")]
        public int ProcessingTime { get; set; }
    }
}
