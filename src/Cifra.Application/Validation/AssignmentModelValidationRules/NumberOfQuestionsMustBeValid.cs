using Cifra.Application.Models.Test.Commands;
using Cifra.Domain.Validation;
using System;

namespace Cifra.Application.Validation.AssignmentModelValidationRules
{
    /// <summary>
    /// Validates the name of a test
    /// </summary>
    public class NumberOfQuestionsMustBeValid : IValidationRule<AddAssignmentCommand>
    {
        private const string Message = "Number of questions must be higher than zero";

        /// <inheritdoc/>
        public ValidationMessage Validate(AddAssignmentCommand model)
        {
            NullChecks(model);

            if (model.NumberOfQuestions <= 0)
            {
                return new ValidationMessage(nameof(model.NumberOfQuestions), Message);
            }
            return null;
        }

        private void NullChecks(AddAssignmentCommand model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
