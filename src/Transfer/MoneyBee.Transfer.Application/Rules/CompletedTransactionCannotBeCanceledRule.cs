namespace MoneyBee.Transfer.Application.Rules
{
    using MoneyBee.Common.Validations;
    using MoneyBee.Transfer.Domain.Enums;

    public class CompletedTransactionCannotBeCanceledRule : IBusinessRule
    {
        private readonly TransactionStatus _transactionStatus;

        public CompletedTransactionCannotBeCanceledRule(TransactionStatus transactionStatus)
        {
            _transactionStatus = transactionStatus;
        }

        public string RuleName => "COMPLETED_TX_CANNOT_CANCEL_VALIDATION";
        public string ErrorMessage => "A completed transaction cannot be canceled.";

        public bool Validate()
        {
            if (_transactionStatus == TransactionStatus.COMPLETED)
            {
                return false;
            }

            return true;
        }
    }
}
