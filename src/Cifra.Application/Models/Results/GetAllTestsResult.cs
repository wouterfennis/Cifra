using System.Collections.Generic;

namespace Cifra.Application.Models.Results
{
    /// <summary>
    /// The result of the Get All Tests operation
    /// </summary>
    public sealed class GetAllTestsResult
    {
        /// <summary>
        /// The tests
        /// </summary>
        public List<Test> Tests { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public GetAllTestsResult(List<Test> tests)
        {
            Tests = tests;
        }
    }
}
