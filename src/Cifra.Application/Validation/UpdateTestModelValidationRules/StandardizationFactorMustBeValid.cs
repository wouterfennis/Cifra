using Cifra.Application.Models.Test.Commands;
using Cifra.Domain.Validation;
using System;

namespace Cifra.Application.Validation.UpdateTestModelValidationRules
{
    /// <summary>
    /// Validates the standardization factor of a test
    /// </summary>
    public class StandardizationFactorMustBeValid : IValidationRule<UpdateTestCommand>
    {
        private const string Message = "Standardization factor must be higher than zero";

        /// <inheritdoc/>
        public ValidationMessage Validate(UpdateTestCommand model)
        {
            NullChecks(model);

            if (model.Test.StandardizationFactor <= 0)
            {
                return new ValidationMessage(nameof(model.Test.StandardizationFactor), Message);
            }
            return null;
        }

        private void NullChecks(UpdateTestCommand model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
