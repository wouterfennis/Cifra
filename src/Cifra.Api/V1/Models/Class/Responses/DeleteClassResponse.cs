using Cifra.Api.V1.Models.Validation;
using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Class.Responses
{
    /// <summary>
    /// The response to delete a Class.
    /// </summary>
    public class DeleteClassResponse
    {
        /// <summary>
        /// The validation messages
        /// </summary>
        public required IEnumerable<ValidationMessage> ValidationMessages { get; init; }
    }
}
