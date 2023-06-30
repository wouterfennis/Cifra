using Cifra.Application.Models.Test.Commands;
using Cifra.Domain.Validation;
using System;

namespace Cifra.Application.Validation.UpdateTestModelValidationRules
{
    /// <summary>
    /// Validates the name of a test
    /// </summary>
    public class NumberOfVersionsMustBeValid : IValidationRule<UpdateTestCommand>
    {
        private const string Message = "Number of versions must be higher than zero";

        /// <inheritdoc/>
        public ValidationMessage Validate(UpdateTestCommand model)
        {
            NullChecks(model);

            if (model.Test.NumberOfVersions <= 0)
            {
                return new ValidationMessage(nameof(model.Test.Name), Message);
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
