namespace MoneyBee.Transfer.Domain.Enums
{
    public enum TransactionStatus : byte
    {
        WAITING_APPROVAL= 0,
        APPROVED = 1,
        COMPLETED = 2,
        CANCELLED = 3,
        REJECTED = 4
    }
}