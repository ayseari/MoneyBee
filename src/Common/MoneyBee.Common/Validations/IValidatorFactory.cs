using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBee.Common.Validations
{
    public interface IValidatorFactory
    {
        /// <summary>
        /// We just return already intiliaized validator.
        /// </summary>
        IBusinessRuleValidator Create(IBusinessRuleValidator validator);
    }
}
