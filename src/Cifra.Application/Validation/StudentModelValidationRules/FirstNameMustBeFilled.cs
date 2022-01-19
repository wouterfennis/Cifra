using Cifra.Application.Models.Class.Commands;
using Cifra.Core.Models.Validation;
using System;

namespace Cifra.Application.Validation.StudentModelValidationRules
{
    /// <summary>
    /// Validates the first name of a student
    /// </summary>
    public class FirstNameMustBeFilled : IValidationRule<AddStudentCommand>
    {
        private const string Message = "First name is required";

        /// <inheritdoc/>
        public ValidationMessage Validate(AddStudentCommand model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.FirstName) || string.IsNullOrWhiteSpace(model.FirstName))
            {
                return new ValidationMessage(nameof(model.FirstName), Message);
            }
            return null;
        }

        private void NullChecks(AddStudentCommand model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
