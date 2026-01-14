namespace MoneyBee.Transfer.Application.Rules
{
    using MoneyBee.Common.Enums;
    using MoneyBee.Common.Validations;

    public class CustomerStatusNotEligibleForSendMoneyRule :IBusinessRule
    {
        private readonly CustomerStatus _customerStatus;

        public CustomerStatusNotEligibleForSendMoneyRule(CustomerStatus customerStatus)
        {
            _customerStatus = customerStatus;
        }

        public string RuleName => "CUSTOMER_STATUS_VALIDATION";
        public string ErrorMessage => "The customer’s current status does not allow money transfers.";

        public bool Validate()
        {
            var blokedStatus = new List<CustomerStatus> { CustomerStatus.Passive, CustomerStatus.Blocked};
            if (blokedStatus.Contains(_customerStatus))
            {
                return false;
            }

            return true;
        }
    }
}
