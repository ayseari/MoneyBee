namespace MoneyBee.Common.Validations.Rules
{
    using System.Text.RegularExpressions;

    public class TaxNumberMustBeValidRule : IBusinessRule
    {
        private readonly string _vkn;

        public TaxNumberMustBeValidRule(string vkn)
        {
            _vkn = vkn;
        }

        public string RuleName => "TAXNUMBER_VALIDATION";
        public string ErrorMessage => "Geçersiz Vergi Kimlik Numarası";

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_vkn))
                return false;

            // Regex: 10 hane
            if (!Regex.IsMatch(_vkn, @"^\d{10}$"))
                return false;

            int[] digits = _vkn.Select(c => c - '0').ToArray();

            int total = 0;

            for (int i = 0; i < 9; i++)
            {
                int tmp = (digits[i] + (9 - i)) % 10;
                int pow = (int)Math.Pow(2, 9 - i);
                int val = (tmp * pow) % 9;

                total += (tmp != 0 && val == 0) ? 9 : val;
            }

            int checkDigit = (10 - (total % 10)) % 10;

            return checkDigit == digits[9];
        }
    }

}
