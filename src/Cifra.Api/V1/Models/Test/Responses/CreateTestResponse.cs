using Cifra.Api.V1.Models.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Test.Responses
{
    /// <summary>
    /// The result of the Create Test operation
    /// </summary>
    public sealed class CreateTestResponse
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
