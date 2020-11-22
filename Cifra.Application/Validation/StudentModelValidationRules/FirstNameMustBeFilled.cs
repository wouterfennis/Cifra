using Cifra.Application.Models.Class.Requests;
using System;

namespace Cifra.Application.Validation.StudentModelValidationRules
{
    public class FirstNameMustBeFilled : IValidationRule<AddStudentRequest>
    {
        private const string Message = "First name is required";
        public ValidationMessage Validate(AddStudentRequest model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.FirstName) || string.IsNullOrWhiteSpace(model.FirstName))
            {
                return new ValidationMessage(nameof(model.FirstName), Message);
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
