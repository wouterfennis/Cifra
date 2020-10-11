using Cifra.Application.Models.Class.Requests;

namespace Cifra.Application.Validation.StudentModelValidationRules
{
    public class NameMustBeFilled : IValidationRule<AddStudentRequest>
    {
        private const string Message = "Not all names are valid";
        public ValidationMessage Validate(AddStudentRequest model)
        {
            if (string.IsNullOrEmpty(model.FullName) || string.IsNullOrWhiteSpace(model.FullName))
            {
                return new ValidationMessage(nameof(model.FullName), Message);
            }
            return null;
        }
    }
}
