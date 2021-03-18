using System;
using System.Collections.Generic;
using Cifra.Application.Validation;

namespace Cifra.Application.Models.Test.Results
{
    /// <summary>
    /// Result of the Add Assignment operation
    /// </summary>
    public sealed class AddBonusAssignmentResult
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
        public AddBonusAssignmentResult(Guid testId, Guid assignmentId)
        {
            TestId = testId;
            AssignmentId = assignmentId;
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public AddBonusAssignmentResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages ?? throw new ArgumentNullException(nameof(validationMessages));
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public AddBonusAssignmentResult(ValidationMessage validationMessage)
        {
            if (validationMessage == null)
            {
                throw new ArgumentNullException(nameof(validationMessage));
            }
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
