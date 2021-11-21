using Cifra.Application.Models.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Class.Results
{
    /// <summary>
    /// Result of the Create Magister Class operation
    /// </summary>
    public sealed class CreateMagisterClassResult
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
        internal CreateMagisterClassResult(int classId)
        {
            ClassId = classId;
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        internal CreateMagisterClassResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages ?? throw new ArgumentNullException(nameof(validationMessages));
        }
    }
}
