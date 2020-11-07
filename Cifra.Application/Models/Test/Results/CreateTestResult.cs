using Cifra.Application.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Test.Results
{
    /// <summary>
    /// The result of the Create Test operation
    /// </summary>
    public sealed class CreateTestResult
    {
        /// <summary>
        /// The Test Id
        /// </summary>
        public Guid TestId { get; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public CreateTestResult(Guid testId)
        {
            TestId = testId;
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public CreateTestResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages ?? throw new ArgumentNullException(nameof(validationMessages));
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public CreateTestResult(ValidationMessage validationMessage)
        {
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
