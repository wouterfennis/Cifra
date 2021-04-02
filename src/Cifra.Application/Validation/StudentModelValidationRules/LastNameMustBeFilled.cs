using Cifra.Application.Models.Class.Requests;
using System;

namespace Cifra.Application.Validation.StudentModelValidationRules
{
    /// <summary>
    /// Validates the last name of a student
    /// </summary>
    public class LastNameMustBeFilled : IValidationRule<AddStudentRequest>
    {
        private const string Message = "Last name is required";

        /// <inheritdoc/>
        public ValidationMessage Validate(AddStudentRequest model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.LastName) || string.IsNullOrWhiteSpace(model.LastName))
            {
                return new ValidationMessage(nameof(model.LastName), Message);
            }
            return null;
        }

        private void NullChecks(AddStudentRequest model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
