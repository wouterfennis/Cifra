using Cifra.Application.Models;
using Cifra.Application.Models.Test.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Validation.TestModelValidationRules
{
    public class NameMustBeFilled : IValidationRule<CreateTestRequest>
    {
        private const string Message = "Name is required";
        public ValidationMessage Validate(CreateTestRequest model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name))
            {
                return new ValidationMessage(nameof(model.Name), Message);
            }
            return null;
        }

        private void NullChecks(CreateTestRequest model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
