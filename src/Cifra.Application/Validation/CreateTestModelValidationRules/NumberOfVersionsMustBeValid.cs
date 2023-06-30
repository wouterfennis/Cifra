using Cifra.Application.Models.Test.Commands;
using Cifra.Domain.Validation;
using System;

namespace Cifra.Application.Validation.CreateTestModelValidationRules
{
    /// <summary>
    /// Validates the name of a test
    /// </summary>
    public class NumberOfVersionsMustBeValid : IValidationRule<CreateTestCommand>
    {
        private const string Message = "Number of versions must be higher than zero";

        /// <inheritdoc/>
        public ValidationMessage Validate(CreateTestCommand model)
        {
            NullChecks(model);

            if (model.NumberOfVersions <= 0)
            {
                return new ValidationMessage(nameof(model.Name), Message);
            }
            return null;
        }

        private void NullChecks(CreateTestCommand model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
