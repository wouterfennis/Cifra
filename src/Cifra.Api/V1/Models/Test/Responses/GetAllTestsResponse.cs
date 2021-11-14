using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Test.Results
{
    /// <summary>
    /// The result of the Get All Tests operation
    /// </summary>
    public sealed class GetAllTestsResponse
    {
        /// <summary>
        /// The tests
        /// </summary>
        public List<Test> Tests { get; set; }
    }
}
