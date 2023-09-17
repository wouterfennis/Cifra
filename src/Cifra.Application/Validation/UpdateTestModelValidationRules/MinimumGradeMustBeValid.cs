using Cifra.Application.Models.Test.Commands;
using Cifra.Domain.Validation;
using System;

namespace Cifra.Application.Validation.UpdateTestModelValidationRules
{
    /// <summary>
    /// Validates the name of a test
    /// </summary>
    public class MinimumGradeMustBeValid : IValidationRule<UpdateTestCommand>
    {
        private const string Message = "Minimum grade must be between 1 and 10";

        /// <inheritdoc/>
        public ValidationMessage Validate(UpdateTestCommand model)
        {
            NullChecks(model);

            if (model.Test.MinimumGrade < 1 || model.Test.MinimumGrade > 10)
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
