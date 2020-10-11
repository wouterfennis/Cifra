using Cifra.Application.Models;
using Cifra.Application.Models.Test.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Validation
{
    public class Validator<T> : IValidator<T>
    {
        private readonly IEnumerable<IValidationRule<T>> validationRules;

        public Validator(IEnumerable<IValidationRule<T>> validationRules)
        {
            this.validationRules = validationRules;
        }

        public IEnumerable<ValidationMessage> ValidateRules(T model)
        {
            var validationMessages = new List<ValidationMessage>();

            foreach (var validationRule in validationRules)
            {
                var result = validationRule.Validate(model);
                if (result != null)
                {
                    validationMessages.Add(result);
                }
            }

            return validationMessages;
        }
    }
}
