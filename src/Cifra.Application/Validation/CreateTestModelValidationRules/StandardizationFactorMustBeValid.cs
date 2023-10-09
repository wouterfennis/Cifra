using Cifra.Application.Models.Test.Commands;
using Cifra.Domain.Validation;
using System;

namespace Cifra.Application.Validation.CreateTestModelValidationRules
{
    /// <summary>
    /// Validates the standardization factor of a test
    /// </summary>
    public class StandardizationFactorMustBeValid : IValidationRule<CreateTestCommand>
    {
        private const string Message = "Standardization factor must be higher than zero";

        /// <inheritdoc/>
        public ValidationMessage Validate(CreateTestCommand model)
        {
            NullChecks(model);

            if (model.StandardizationFactor <= 0)
            {
                return new ValidationMessage(nameof(model.StandardizationFactor), Message);
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
