using Cifra.Application.Models.Test.Requests;

namespace Cifra.Application.Validation.QuestionModelValidationRules
{
    public class NameMustBeFilled : IValidationRule<AddQuestionRequest>
    {
        private const string Message = "Not all names are valid";
        public ValidationMessage Validate(AddQuestionRequest model)
        {
            foreach (string name in model.Names)
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                {
                    return new ValidationMessage(nameof(name), Message);
                }
            }
            return null;
        }
    }
}
