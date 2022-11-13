namespace Cifra.Application.Models.Test.Results
{
    /// <summary>
    /// The result of the Get Test operation
    /// </summary>
    public sealed class GetTestResult
    {
        /// <summary>
        /// The test
        /// </summary>
        public Domain.Test Test { get; }
        
        /// <summary>
        /// Ctor
        /// </summary>
        public GetTestResult(Domain.Test test)
        {
            Test = test;
        }
    }
}
