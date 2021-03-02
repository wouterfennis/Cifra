using System;
using Cifra.Application.Models.Test.Requests;

namespace Cifra.Application.Validation.TestModelValidationRules
{
    /// <summary>
    /// Validates the name of a test
    /// </summary>
    public class NumberOfVersionsMustBeValid : IValidationRule<CreateTestRequest>
    {
        private const string Message = "Number of versions must be higher than zero";

        /// <inheritdoc/>
        public ValidationMessage Validate(CreateTestRequest model)
        {
            NullChecks(model);

            if (model.NumberOfVersions <= 0)
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
