namespace MoneyBee.Transfer.Application.Models.Settings
{
    /// <summary>
    /// Money Transfer Api Settings
    /// </summary>
    public class MoneyTransferSettings
    {
        /// <summary>
        /// Gets or sets the section name 
        /// </summary>
        public const string SectionName = "MoneyTransferSettings";

        /// <summary>
        /// Daily Transfer Limit
        /// </summary>
        public decimal DailyTransferLimit { get; set; }

        /// <summary>
        /// High Amount Threshold
        /// </summary>
        public decimal HighAmountThreshold { get; set; }

        /// <summary>
        /// Rate of fee
        /// </summary>
        public decimal FeeRate { get; set; }
    }
}
