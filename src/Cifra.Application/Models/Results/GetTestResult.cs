using Cifra.Domain;

namespace Cifra.Application.Models.Results
{
    /// <summary>
    /// The result of the Get Test operation
    /// </summary>
    public sealed class GetTestResult
    {
        /// <summary>
        /// The test
        /// </summary>
        public Test Test { get; }
        
        /// <summary>
        /// Ctor
        /// </summary>
        public GetTestResult(Test test)
        {
            Test = test;
        }
    }
}
