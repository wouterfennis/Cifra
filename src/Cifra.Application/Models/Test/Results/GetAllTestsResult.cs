using System.Collections.Generic;

namespace Cifra.Application.Models.Test.Results
{
    /// <summary>
    /// The result of the Get All Tests operation
    /// </summary>
    public sealed class GetAllTestsResult
    {
        /// <summary>
        /// The tests
        /// </summary>
        public List<Core.Models.Test.Test> Tests { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public GetAllTestsResult(List<Core.Models.Test.Test> tests)
        {
            Tests = tests;
        }
    }
}
