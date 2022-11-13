using System;

namespace Cifra.Application.Models.Class.Results
{
    /// <summary>
    /// The result of the Get Class operation
    /// </summary>
    public sealed class GetClassResult
    {
        /// <summary>
        /// The class
        /// </summary>
        public Domain.Class RetrievedClass { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        internal GetClassResult(Domain.Class retrievedClass)
        {
            RetrievedClass = retrievedClass ?? throw new ArgumentNullException(nameof(retrievedClass));
        }
    }
}
