using System.Collections.Generic;

namespace Cifra.Api.Models.Test.Results
{
    /// <summary>
    /// The result of the Get All Tests operation
    /// </summary>
    public sealed class GetAllTestsResponse
    {
        /// <summary>
        /// The tests
        /// </summary>
        public IEnumerable<Test> Tests { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public GetAllTestsResponse(IEnumerable<Test> tests)
        {
            Tests = tests;
        }
    }
}
