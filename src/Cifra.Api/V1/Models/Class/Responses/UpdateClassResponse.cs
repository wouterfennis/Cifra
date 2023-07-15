using Cifra.Api.V1.Models.Validation;
using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Class.Responses
{
    public class UpdateClassResponse
    {
        /// <summary>
        /// The Class Id
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; set; }
    }
}
