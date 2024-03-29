﻿
using System.ComponentModel.DataAnnotations;

namespace Cifra.Api.V1.Models.Test.Requests
{
    /// <summary>
    /// The request to delete a test
    /// </summary>
    public sealed class DeleteTestRequest
    {
        /// <summary>
        /// The Id of the test that should be deleted.
        /// </summary>
        [Required]
        public uint TestId { get; init; }
    }
}