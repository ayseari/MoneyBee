namespace MoneyBee.Common.Helpers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Api Key Helper
    /// </summary>
    public static class ApiKeyHelper
    {
        /// <summary>
        /// Generate a new API key
        /// </summary>
        public static string GenerateSecureApiKey()
        {
            const string prefix = "mb_";
            var bytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            var base64Key = Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
            return $"{prefix}{base64Key}";
        }

        /// <summary>
        /// Hash API key using SHA256
        /// </summary>
        public static string HashApiKey(string apiKey)
        {
            var bytes = Encoding.UTF8.GetBytes(apiKey);
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLowerInvariant();
        }
    }
}
