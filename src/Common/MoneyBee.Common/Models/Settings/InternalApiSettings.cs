namespace MoneyBee.Common.Models.Settings
{
    /// <summary>
    /// Internal Api Settings
    /// </summary>
    public class InternalApiSettings
    {
        /// <summary>
        /// Gets or sets the section name 
        /// </summary>
        public const string SectionName = "InternalApiSettings";

        /// <summary>
        /// Customer Api Base Url
        /// </summary>
        public string CustomerApiBaseUrl { get; set; }
    }
}
