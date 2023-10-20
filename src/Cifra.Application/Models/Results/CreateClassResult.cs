using Cifra.Application.Models.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Results
{
    /// <summary>
    /// Result of the Create Class operation
    /// </summary>
    public sealed class CreateClassResult
    {
        /// <summary>
        /// The Class Id
        /// </summary>
        public uint ClassId { get; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public CreateClassResult(uint classId)
        {
            ClassId = classId;
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public CreateClassResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages ?? throw new ArgumentNullException(nameof(validationMessages));
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public CreateClassResult(ValidationMessage validationMessage)
        {
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
