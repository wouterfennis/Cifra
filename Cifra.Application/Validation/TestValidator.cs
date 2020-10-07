using Cifra.Application.Models;
using Cifra.Application.Models.Test.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Validation
{
    public class TestValidator : IValidator<CreateTestRequest>
    {
        private readonly IEnumerable<IValidationRule<CreateTestRequest>> validationRules;

        public TestValidator(IEnumerable<IValidationRule<CreateTestRequest>> validationRules)
        {
            this.validationRules = validationRules;
        }

        public IEnumerable<ValidationMessage> ValidateRules(CreateTestRequest model)
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
