using Cifra.Application.Validation;
using System.Collections.Generic;

namespace Cifra.Application.Models.Test.Results
{
    /// <summary>
    /// Result of the Add Question operation
    /// </summary>
    public sealed class AddQuestionResult
    {
        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public AddQuestionResult()
        {
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public AddQuestionResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public AddQuestionResult(ValidationMessage validationMessage)
        {
            ValidationMessages = new List<ValidationMessage> { validationMessage };
        }
    }
}
