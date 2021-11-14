namespace Cifra.Api.V1.Models.Validation
{
    /// <summary>
    /// A validation message
    /// </summary>
    public class ValidationMessage
    {
        /// <summary>
        /// The field where the validation took place
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// The message of the validation result
        /// </summary>
        public string Message { get; set; }
    }
}