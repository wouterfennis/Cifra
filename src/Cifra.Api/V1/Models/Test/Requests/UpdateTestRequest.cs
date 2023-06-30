using System.ComponentModel.DataAnnotations;

namespace Cifra.Api.V1.Models.Test.Requests
{
    /// <summary>
    /// The request to update a test
    /// </summary>
    public sealed class UpdateTestRequest
    {
        /// <summary>
        /// The test.
        /// </summary>
        public Test Test { get; set; }
    }
}