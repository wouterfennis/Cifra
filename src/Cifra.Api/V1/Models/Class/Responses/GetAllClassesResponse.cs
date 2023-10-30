using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Class.Responses
{
    /// <summary>
    /// Response to get all classes.
    /// </summary>
    public class GetAllClassesResponse
    {
        /// <summary>
        /// The classes
        /// </summary>
        public required IEnumerable<Class> Classes { get; init; }
    }
}
