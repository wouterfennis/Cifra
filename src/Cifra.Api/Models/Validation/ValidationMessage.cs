namespace Cifra.Api.Models.Validation
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
    }
}