using Cifra.Application.Models.Test.Requests;
using System;

namespace Cifra.Application.Validation.TestModelValidationRules
{
    /// <summary>
    /// Validates the name of a test
    /// </summary>
    public class NameMustBeFilled : IValidationRule<CreateTestRequest>
    {
        private const string Message = "Name is required";

        /// <inheritdoc/>
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
