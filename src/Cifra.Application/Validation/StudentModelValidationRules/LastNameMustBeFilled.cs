using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Validation;
using System;

namespace Cifra.Application.Validation.StudentModelValidationRules
{
    /// <summary>
    /// Validates the last name of a student
    /// </summary>
    public class LastNameMustBeFilled : IValidationRule<AddStudentCommand>
    {
        private const string Message = "Last name is required";

        /// <inheritdoc/>
        public ValidationMessage Validate(AddStudentCommand model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.LastName) || string.IsNullOrWhiteSpace(model.LastName))
            {
                return new ValidationMessage(nameof(model.LastName), Message);
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
