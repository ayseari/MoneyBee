namespace MoneyBee.Common.Validations
{
    public interface IBusinessRule
    {
        string RuleName { get; }
        string ErrorMessage { get; }
        bool Validate();
    }
}
