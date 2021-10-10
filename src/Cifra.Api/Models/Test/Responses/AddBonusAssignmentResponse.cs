using Cifra.Api.Models.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Api.Models.Test.Responses
{
    /// <summary>
    /// Result of the Add Assignment operation
    /// </summary>
    public sealed class AddBonusAssignmentResponse
    {
        /// <summary>
        /// The test Id
        /// </summary>
        public int? TestId { get; }

        /// <summary>
        /// The assignment Id
        /// </summary>
        public int? AssignmentId { get; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public AddBonusAssignmentResponse(int testId, int assignmentId)
        {
            TestId = testId;
            AssignmentId = assignmentId;
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public AddBonusAssignmentResponse(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages ?? throw new ArgumentNullException(nameof(validationMessages));
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public AddBonusAssignmentResponse(ValidationMessage validationMessage)
        {
            if (validationMessage == null)
            {
                throw new ArgumentNullException(nameof(validationMessage));
            }
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
