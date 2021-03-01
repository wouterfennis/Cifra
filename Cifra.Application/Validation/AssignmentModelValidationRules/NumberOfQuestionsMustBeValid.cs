using System;
using Cifra.Application.Models.Test.Requests;

namespace Cifra.Application.Validation.AssignmentModelValidationRules
{
    /// <summary>
    /// Validates the name of a test
    /// </summary>
    public class NumberOfQuestionsMustBeValid : IValidationRule<AddAssignmentRequest>
    {
        private const string Message = "Number of questions must be higher than zero";

        /// <inheritdoc/>
        public ValidationMessage Validate(AddAssignmentRequest model)
        {
            NullChecks(model);

            if (model.NumberOfQuestions <= 0)
            {
                return new ValidationMessage(nameof(model.NumberOfQuestions), Message);
            }
            return null;
        }

        private void NullChecks(AddAssignmentRequest model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
