using Cifra.Api.V1.Models.Validation;
using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Class.Responses
{
    /// <summary>
    /// Result of the Add Students operation.
    /// </summary>
    public class AddStudentsResponse
    {
        /// <summary>
        /// The validation messages.
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; set; }
    }
}
