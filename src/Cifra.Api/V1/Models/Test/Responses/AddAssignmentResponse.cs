using Cifra.Api.V1.Models.Validation;
using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Test.Responses
{
    /// <summary>
    /// Result of the Add Assignment operation
    /// </summary>
    public sealed class AddAssignmentResponse
    {
        /// <summary>
        /// The test Id
        /// </summary>
        public int? TestId { get; set; }

        /// <summary>
        /// The assignment Id
        /// </summary>
        public int? AssignmentId { get; set; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; set; }
    }
}
