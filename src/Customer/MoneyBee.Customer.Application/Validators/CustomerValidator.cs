namespace MoneyBee.Customer.Application.Validators
{
    using MoneyBee.Common.Enums;
    using MoneyBee.Common.Validations;
    using MoneyBee.Common.Validations.Rules;
    using MoneyBee.Customer.Application.Rules;

    public class CustomerValidator : BusinessRuleValidator
    {
        public CustomerValidator(
            string tckn,
            string taxNumber,
            string phoneNumber,
            DateTime? dateOfBirth,
            CustomerType customerType)
        {
            // TCKN (şahıs müşteri)
            if (!string.IsNullOrWhiteSpace(tckn) && customerType == CustomerType.Individual)
            {
                AddRule(new TcknMustBeValidRule(tckn));
            }

            // VKN (tüzel müşteri)
            if (!string.IsNullOrWhiteSpace(taxNumber) && customerType == CustomerType.Corporate)
            {
                AddRule(new TaxNumberMustBeValidRule(taxNumber));
            }

            // Telefon
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                AddRule(new PhoneNumberMustBeValidRule(phoneNumber));
            }

            // Yaş >= 18
            if (dateOfBirth.HasValue)
            {
                AddRule(new AgeMustBeAtLeast18Rule(dateOfBirth.Value));
            }
        }
    }

}
