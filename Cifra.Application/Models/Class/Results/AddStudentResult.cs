
using Cifra.Application.Validation;
using System.Collections.Generic;

namespace Cifra.Application.Models.Class.Results
{
    /// <summary>
    /// Result of the Add Student operation
    /// </summary>
    public sealed class AddStudentResult
    {
        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        internal AddStudentResult()
        {
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        internal AddStudentResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        internal AddStudentResult(ValidationMessage validationMessage)
        {
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
