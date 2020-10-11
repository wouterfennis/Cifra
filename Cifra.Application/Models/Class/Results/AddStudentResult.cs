
using Cifra.Application.Validation;
using System.Collections.Generic;

namespace Cifra.Application.Models.Class.Results
{
    public class AddStudentResult
    {
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        public AddStudentResult()
        {
            ValidationMessages = new List<ValidationMessage>();
        }

        public AddStudentResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages;
        }

        public AddStudentResult(ValidationMessage validationMessage)
        {
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
