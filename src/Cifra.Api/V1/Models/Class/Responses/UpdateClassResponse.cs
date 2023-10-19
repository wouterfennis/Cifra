using Cifra.Api.V1.Models.Validation;
using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Class.Responses
{
    public class UpdateClassResponse
    {
        /// <summary>
        /// The Class Id
        /// </summary>
        public uint ClassId { get; init; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; init; }
    }
}
