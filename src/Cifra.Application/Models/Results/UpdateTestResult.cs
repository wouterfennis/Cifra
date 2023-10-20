using Cifra.Application.Models.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Results
{
    /// <summary>
    /// The result of the Update Test operation
    /// </summary>
    public sealed class UpdateTestResult
    {
        /// <summary>
        /// The Test Id
        /// </summary>
        public uint TestId { get; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public UpdateTestResult(uint testId)
        {
            TestId = testId;
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public UpdateTestResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages ?? throw new ArgumentNullException(nameof(validationMessages));
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public UpdateTestResult(ValidationMessage validationMessage)
        {
            if (validationMessage == null)
            {
                throw new ArgumentNullException(nameof(validationMessage));
            }
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
