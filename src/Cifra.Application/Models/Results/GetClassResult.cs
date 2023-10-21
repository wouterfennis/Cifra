using Cifra.Domain;
using System;

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
        public Class RetrievedClass { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public GetClassResult(Class retrievedClass)
        {
            RetrievedClass = retrievedClass ?? throw new ArgumentNullException(nameof(retrievedClass));
        }
    }
}
