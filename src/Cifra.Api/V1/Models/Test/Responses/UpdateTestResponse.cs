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
        public required uint TestId { get; init; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public required List<ValidationMessage> ValidationMessages { get; init; }
    }
}
