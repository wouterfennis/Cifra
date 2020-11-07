using Cifra.Application.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Test.Results
{
    /// <summary>
    /// Result of the Add Assignment operation
    /// </summary>
    public sealed class AddAssignmentResult
    {
        /// <summary>
        /// The test Id
        /// </summary>
        public Guid? TestId { get; }

        /// <summary>
        /// The assignment Id
        /// </summary>
        public Guid? AssignmentId { get; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public AddAssignmentResult(Guid testId, Guid assignmentId)
        {
            TestId = testId;
            AssignmentId = assignmentId;
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public AddAssignmentResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public AddAssignmentResult(ValidationMessage validationMessage)
        {
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
