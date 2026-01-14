namespace MoneyBee.Common.Services.Interfaces
{
    using System.Text.RegularExpressions;

    //TODO: silinecek

    public class PhoneNumberValidationService : IPhoneNumberValidationService
    {
        // E.164: +905551234567
        private static readonly Regex E164Regex =
            new(@"^\+[1-9]\d{1,14}$", RegexOptions.Compiled);

        public bool IsValid(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            phoneNumber = Normalize(phoneNumber);

            return E164Regex.IsMatch(phoneNumber);
        }

        public string Normalize(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return string.Empty;

            // Boşluk, parantez, tire temizle
            phoneNumber = phoneNumber
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "");

            // Türkiye özel normalize
            if (phoneNumber.StartsWith("05"))
                phoneNumber = "+9" + phoneNumber;

            if (phoneNumber.StartsWith("5"))
                phoneNumber = "+90" + phoneNumber;

            if (!phoneNumber.StartsWith("+"))
                phoneNumber = "+" + phoneNumber;

            return phoneNumber;
        }
    }
}

