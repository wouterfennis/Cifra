using Cifra.Domain.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Class.Results
{
    /// <summary>
    /// Result of the Update Class operation
    /// </summary>
    public sealed class UpdateClassResult
    {
        /// <summary>
        /// The Class Id
        /// </summary>
        public int ClassId { get; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public UpdateClassResult(int classId)
        {
            ClassId = classId;
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public UpdateClassResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages ?? throw new ArgumentNullException(nameof(validationMessages));
        }
    }
}
