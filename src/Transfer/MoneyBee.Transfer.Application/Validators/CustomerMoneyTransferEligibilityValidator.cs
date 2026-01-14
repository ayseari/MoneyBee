using MoneyBee.Common.Enums;
using MoneyBee.Common.Validations;
using MoneyBee.Transfer.Application.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBee.Transfer.Application.Validators
{
    public class CustomerMoneyTransferEligibilityValidator : BusinessRuleValidator
    {
        public CustomerMoneyTransferEligibilityValidator(CustomerStatus customerStatus)
        {
            AddRule(new CustomerStatusNotEligibleForSendMoneyRule(customerStatus));
        }
    }
}
