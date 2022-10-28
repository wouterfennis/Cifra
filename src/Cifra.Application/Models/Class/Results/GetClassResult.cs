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
        public Core.Models.Class.Class RetrievedClass { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        internal GetClassResult(Core.Models.Class.Class retrievedClass)
        {
            RetrievedClass = retrievedClass ?? throw new ArgumentNullException(nameof(retrievedClass));
        }
    }
}
