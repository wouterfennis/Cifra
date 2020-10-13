using Cifra.Application.Models.Class.Requests;
using System;

namespace Cifra.Application.Validation.StudentModelValidationRules
{
    public class NameMustBeFilled : IValidationRule<AddStudentRequest>
    {
        private const string Message = "Name is required";
        public ValidationMessage Validate(AddStudentRequest model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.FullName) || string.IsNullOrWhiteSpace(model.FullName))
            {
                return new ValidationMessage(nameof(model.FullName), Message);
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
