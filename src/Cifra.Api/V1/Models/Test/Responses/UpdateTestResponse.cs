using Cifra.Api.V1.Models.Validation;
using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Test.Responses
{
    /// <summary>
    /// The result of the Update Test operation
    /// </summary>
    public sealed class UpdateTestResponse
    {
        /// <summary>
        /// The Test Id
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public List<ValidationMessage> ValidationMessages { get; set; }
    }
}
