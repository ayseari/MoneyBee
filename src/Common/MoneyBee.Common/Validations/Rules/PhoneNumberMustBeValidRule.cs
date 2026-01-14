namespace MoneyBee.Common.Validations.Rules
{
    using System.Text.RegularExpressions;

    public class PhoneNumberMustBeValidRule : IBusinessRule
    {
        private readonly string _phoneNumber;

        public PhoneNumberMustBeValidRule(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public string RuleName => "PHONE_NUMBER_VALIDATION";
        public string ErrorMessage => "Geçersiz telefon numarası";

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_phoneNumber))
                return false;

            // E.164 regex
            return Regex.IsMatch(_phoneNumber, @"^\+[1-9]\d{1,14}$");
        }
    }

}
