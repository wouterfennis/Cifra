using Cifra.Application.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Test.Results
{
    public class CreateTestResult
    {
        public Test Test { get; }

        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        public CreateTestResult(Test test)
        {
            Test = test;
            ValidationMessages = new List<ValidationMessage>();
        }

        public CreateTestResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages ?? throw new ArgumentNullException(nameof(validationMessages));
        }

        public CreateTestResult(ValidationMessage validationMessage)
        {
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
