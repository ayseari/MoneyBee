namespace MoneyBee.Common.Validations
{
    public class ValidatorFactory : IValidatorFactory
    {
        /// <summary>
        /// We just return already intiliaized validator.
        /// </summary>
        public IBusinessRuleValidator Create(IBusinessRuleValidator validator)
        {
            return validator;
        }
    }
}
