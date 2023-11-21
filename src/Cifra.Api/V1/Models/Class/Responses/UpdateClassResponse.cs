using Cifra.Api.V1.Models.Validation;
using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Class.Responses
{
    /// <summary>
    /// The response to update a Class.
    /// </summary>
    public class UpdateClassResponse
    {
        /// <summary>
        /// The Class Id
        /// </summary>
        public required uint ClassId { get; init; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public required IEnumerable<ValidationMessage> ValidationMessages { get; init; }
    }
}
