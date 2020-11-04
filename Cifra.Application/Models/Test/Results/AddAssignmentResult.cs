using Cifra.Application.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Test.Results
{
    public sealed class AddAssignmentResult
    {
        public Guid? TestId { get; }
        public Guid? AssignmentId { get; }

        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        public AddAssignmentResult(Guid testId, Guid assignmentId)
        {
            TestId = testId;
            AssignmentId = assignmentId;
            ValidationMessages = new List<ValidationMessage>();
        }

        public AddAssignmentResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages;
        }

        public AddAssignmentResult(ValidationMessage validationMessage)
        {
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
