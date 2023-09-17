using Cifra.Application.Models.Test.Commands;
using Cifra.Domain.Validation;
using System;

namespace Cifra.Application.Validation.CreateTestModelValidationRules
{
    /// <summary>
    /// Validates the name of a test
    /// </summary>
    public class MinimumGradeMustBeValid : IValidationRule<CreateTestCommand>
    {
        private const string Message = "Minimum grade must be between 1 and 10";

        /// <inheritdoc/>
        public ValidationMessage Validate(CreateTestCommand model)
        {
            NullChecks(model);

            if (model.MinimumGrade < 1 || model.MinimumGrade > 10)
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
