using Cifra.Domain;

namespace Cifra.Application.Models.Results
{
    /// <summary>
    /// The result of the Get Class operation
    /// </summary>
    public sealed class GetClassResult
    {
        /// <summary>
        /// The class
        /// </summary>
        public required Class? RetrievedClass { get; init; }
    }
}
