using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Class.Responses
{
    public class GetAllClassesResponse
    {
        /// <summary>
        /// The classes
        /// </summary>
        public IEnumerable<Class> Classes { get; set; }
    }
}
