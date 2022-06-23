using System;

namespace Cifra.Core.Models.Validation
{
    /// <summary>
    /// A validation message
    /// </summary>
    public class ValidationMessage
    {
        /// <summary>
        /// The field where the validation took place
        /// </summary>
        public string Field { get; }

        /// <summary>
        /// The message of the validation result
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public ValidationMessage(string field, string message)
        {
            Field = field;
            Message = message;
        }

        /// <summary>
        /// Static way to create <see cref="ValidationMessage"/>.
        /// </summary>
        public static ValidationMessage Create(string field, string message)
        {
            return new ValidationMessage(field, message);
        }
    }
}