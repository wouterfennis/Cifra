using Cifra.Domain.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Results
{
    /// <summary>
    /// Result of the Delete Class operation
    /// </summary>
    public sealed class DeleteClassResult
    {
        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        public DeleteClassResult()
        {
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public DeleteClassResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages ?? throw new ArgumentNullException(nameof(validationMessages));
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public DeleteClassResult(ValidationMessage validationMessage)
        {
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
