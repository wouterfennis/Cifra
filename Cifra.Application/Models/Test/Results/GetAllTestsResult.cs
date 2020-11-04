using System.Collections.Generic;

namespace Cifra.Application.Models.Test.Results
{
    public sealed class GetAllTestsResult
    {
        public IEnumerable<Test> Tests { get; }

        public GetAllTestsResult(IEnumerable<Test> tests)
        {
            Tests = tests;
        }
    }
}
