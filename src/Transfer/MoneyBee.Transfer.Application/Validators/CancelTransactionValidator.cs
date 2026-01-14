namespace MoneyBee.Transfer.Application.Validators
{
    using MoneyBee.Common.Enums;
    using MoneyBee.Common.Validations;
    using MoneyBee.Transfer.Application.Rules;
    using MoneyBee.Transfer.Domain.Enums;

    /// <summary>
    /// Cancel Transaction Validator
    /// </summary>
    public class CancelTransactionValidator : BusinessRuleValidator
    {
        public CancelTransactionValidator(TransactionStatus transactionStatus)
        {
            AddRule(new CompletedTransactionCannotBeCanceledRule(transactionStatus));
            AddRule(new TransactionAlreadyCanceledRule(transactionStatus));
        }
    }
}
