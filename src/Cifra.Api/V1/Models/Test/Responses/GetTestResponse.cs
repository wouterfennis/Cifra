using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Test.Results
{
    /// <summary>
    /// The result of the Get Test operation
    /// </summary>
    public sealed class GetTestResponse
    {
        /// <summary>
        /// The test
        /// </summary>
        public Test Test { get; set; }
    }
}
