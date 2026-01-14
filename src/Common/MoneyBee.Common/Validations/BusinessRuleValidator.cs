namespace MoneyBee.Common.Validations
{
    public class BusinessRuleValidator : IBusinessRuleValidator
    {
        private readonly List<IBusinessRule> _rules;

        public BusinessRuleValidator()
        {
            _rules = new List<IBusinessRule>();
        }

        public BusinessRuleValidator AddRule(IBusinessRule rule)
        {
            _rules.Add(rule);
            return this;
        }

        public ValidationResult Validate()
        {
            var result = new ValidationResult();

            foreach (var rule in _rules)
            {
                if (!rule.Validate())
                {
                    result.AddError(rule.RuleName, rule.ErrorMessage);
                }
            }

            return result;
        }

    }
}