namespace MoneyBee.Customer.Application.Rules
{
    using MoneyBee.Common.Validations;
    using System;

    /// <summary>
    /// 18 yaş kontrolü için iş kuralı
    /// </summary>
    public class AgeMustBeAtLeast18Rule : IBusinessRule
    {
        private readonly DateTime _dateOfBirth;

        public AgeMustBeAtLeast18Rule(DateTime dateOfBirth)
        {
            _dateOfBirth = dateOfBirth;
        }

        public string RuleName => "AGE_VALIDATION";
        public string ErrorMessage => "Müşteri 18 yaşından küçük olamaz";//TODO ingilizce yap

        public bool Validate()
        {
            var age = DateTime.Now.Year - _dateOfBirth.Year;

            if (DateTime.Now.Month < _dateOfBirth.Month ||
                (DateTime.Now.Month == _dateOfBirth.Month && DateTime.Now.Day < _dateOfBirth.Day))
            {
                age--;
            }

            return age >= 18;
        }
    }
}
