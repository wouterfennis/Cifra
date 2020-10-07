using Cifra.Application.Validation;
using System.Collections.Generic;

namespace Cifra.Application.Models.Test.Results
{
    public class AddQuestionResult
    {
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        public AddQuestionResult()
        {
            ValidationMessages = new List<ValidationMessage>();
        }

        public AddQuestionResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages;
        }

        public AddQuestionResult(ValidationMessage validationMessage)
        {
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
