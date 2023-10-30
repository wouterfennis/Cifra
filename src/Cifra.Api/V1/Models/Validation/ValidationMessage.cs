﻿namespace Cifra.Api.V1.Models.Validation
{
    /// <summary>
    /// A validation message
    /// </summary>
    public class ValidationMessage
    {
        /// <summary>
        /// The field where the validation took place
        /// </summary>
        public required string Field { get; init; }

        /// <summary>
        /// The message of the validation result
        /// </summary>
        public required string Message { get; init; }
    }
}