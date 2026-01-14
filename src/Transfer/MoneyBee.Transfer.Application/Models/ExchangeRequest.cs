namespace MoneyBee.Transfer.Application.Models
{
    public class ExchangeRequest
    {
        public ExchangeRequest(string fromCurreny, string toCurreny, decimal amount)
        {
            FromCurreny = fromCurreny;
            ToCurreny = toCurreny;
            Amount = amount;
        }

        public string FromCurreny { get; set; }
        public string ToCurreny { get; set; }
        public decimal Amount { get; set; }
    }
}
