﻿using Cifra.Api.V1.Models.Validation;
using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Class.Responses
{
    public class CreateClassResponse
    {
        /// <summary>
        /// The Class Id
        /// </summary>
        public int ClassId { get; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }
    }
}
