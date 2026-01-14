namespace MoneyBee.Common.Utilities
{
    public static class PhoneNumberNormalizeHelper
    {
        public static string Normalize(string phoneNumber, string defaultCountryCode = "90")
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return string.Empty;

            // Temizlik
            phoneNumber = phoneNumber
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "");

            // + ile başlıyorsa dokunma
            if (phoneNumber.StartsWith("+"))
                return phoneNumber;

            // 00 ile başlayan uluslararası format
            if (phoneNumber.StartsWith("00"))
                return "+" + phoneNumber.Substring(2);

            // Türkiye özel (05xxxxxxxxx)
            if (phoneNumber.StartsWith("0"))
                return $"+{defaultCountryCode}{phoneNumber.Substring(1)}";

            // Düz numara (5xxxxxxxxx)
            return $"+{defaultCountryCode}{phoneNumber}";
        }
    }

}
