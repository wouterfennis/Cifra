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
        public Core.Models.Test.Test Test { get; }
        
        /// <summary>
        /// Ctor
        /// </summary>
        public GetTestResult(Core.Models.Test.Test test)
        {
            Test = test;
        }
    }
}
