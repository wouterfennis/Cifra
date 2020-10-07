using Cifra.Application.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Class.Results
{
    public class CreateClassResult
    {
        public Class Class { get; }

        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        public CreateClassResult(Class @class)
        {
            Class = @class;
            ValidationMessages = new List<ValidationMessage>();
        }

        public CreateClassResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages ?? throw new ArgumentNullException(nameof(validationMessages));
        }

        public CreateClassResult(ValidationMessage validationMessage)
        {
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
