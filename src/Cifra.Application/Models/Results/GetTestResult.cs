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
        public required Test? Test { get; init; }
    }
}
