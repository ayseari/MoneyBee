namespace MoneyBee.Common.Validations.Rules
{
    using System.Text.RegularExpressions;

    public class TcknMustBeValidRule : IBusinessRule
    {

        private readonly string _tckn;

        public TcknMustBeValidRule(string tckn)
        {
            _tckn = tckn;
        }

        public string RuleName => "TCKN_VALIDATION";
        public string ErrorMessage => "Geçersiz T.C. Kimlik Numarası";

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_tckn))
                return false;

            // Regex kontrolü
            if (!Regex.IsMatch(_tckn, @"^[1-9]\d{10}$"))
                return false;

            int[] digits = _tckn.Select(c => c - '0').ToArray();

            // 10. hane kontrolü
            int oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            int evenSum = digits[1] + digits[3] + digits[5] + digits[7];

            int digit10 = ((oddSum * 7) - evenSum) % 10;
            if (digit10 != digits[9])
                return false;

            // 11. hane kontrolü
            int sum10 = digits.Take(10).Sum();
            int digit11 = sum10 % 10;

            return digit11 == digits[10];
        }
    }
}