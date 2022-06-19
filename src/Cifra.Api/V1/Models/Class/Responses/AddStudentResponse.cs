using Cifra.Api.V1.Models.Validation;
using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Class.Responses
{
    /// <summary>
    /// Result of the Add Student operation.
    /// </summary>
    public class AddStudentResponse
    {
        /// <summary>
        /// The validation messages.
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }
    }
}
