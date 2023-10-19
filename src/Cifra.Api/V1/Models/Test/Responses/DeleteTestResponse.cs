using Cifra.Api.V1.Models.Validation;
using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Test.Results
{
    /// <summary>
    /// The result of the DELETE Test operation
    /// </summary>
    public sealed class DeleteTestResponse
    {
        /// <summary>
        /// The validation messages
        /// </summary>
        public List<ValidationMessage> ValidationMessages { get; init; }
    }
}
