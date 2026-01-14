using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBee.Common.Validations
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<ValidationError> Errors { get; set; }

        public ValidationResult()
        {
            Errors = new List<ValidationError>();
            IsValid = true;
        }

        public void AddError(string ruleName, string errorMessage)
        {
            Errors.Add(new ValidationError
            {
                RuleName = ruleName,
                ErrorMessage = errorMessage
            });
            IsValid = false;
        }
    }
}
