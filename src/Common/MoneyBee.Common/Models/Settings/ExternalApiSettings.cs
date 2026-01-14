namespace MoneyBee.Common.Models.Settings
{
    /// <summary>
    /// External Api Settings
    /// </summary>
    public class ExternalApiSettings
    {
        /// <summary>
        /// Gets or sets the section name 
        /// </summary>
        public const string SectionName = "ExternalApiSettings";

        /// <summary>
        /// KycService Api Base Url
        /// </summary>
        public string KycApiBaseUrl { get; set; }

        /// <summary>
        /// Exchange Rate Service Api Base Url
        /// </summary>
        public string ExchangeRateApiBaseUrl { get; set; }

        /// <summary>
        /// Fraud Detection Service Api Base Url
        /// </summary>
        public string FraudDetectionApiBaseUrl { get; set; }
    }
}
