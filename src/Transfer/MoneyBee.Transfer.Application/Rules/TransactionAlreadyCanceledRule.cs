namespace MoneyBee.Transfer.Application.Rules
{
    using MoneyBee.Common.Validations;
    using MoneyBee.Transfer.Domain.Enums;

    public class TransactionAlreadyCanceledRule : IBusinessRule
    {
        private readonly TransactionStatus _transactionStatus;

        public TransactionAlreadyCanceledRule(TransactionStatus transactionStatus)
        {
            _transactionStatus = transactionStatus;
        }

        public string RuleName => "TX_ALREADY_CANCELED_VALIDATION";
        public string ErrorMessage => "Transaction already canceled";

        public bool Validate()
        {
            if (_transactionStatus == TransactionStatus.CANCELLED)
            {
                return false;
            }

            return true;
        }
    }
}
