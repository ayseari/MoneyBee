namespace MoneyBee.External.Api.Client.Models.Kyc
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Kyc Verification Request
    /// </summary>
    public class KycVerifyRequest
    {
        /// <summary>
        /// User unique identifier
        /// </summary>
        [Required]
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Turkish National ID (11 digits)
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TCNO must be exactly 11 digits")]
        [JsonProperty("tcno")]
        public string TcNo { get; set; } = string.Empty;

        /// <summary>
        /// Birth year
        /// </summary>
        [Required]
        [Range(1900, 2024, ErrorMessage = "Birth year must be between 1900 and 2024")]
        [JsonProperty("birthYear")]
        public int BirthYear { get; set; }

        /// <summary>
        /// User first name (optional)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// User last name (optional)
        /// </summary>
        [JsonProperty("surname")]
        public string Surname { get; set; }
    }
}
