using System.ComponentModel.DataAnnotations;

namespace Cifra.Api.V1.Models.Test.Requests
{
    /// <summary>
    /// The request to create a test
    /// </summary>
    public sealed class CreateTestRequest
    {
        /// <summary>
        /// The name of the test
        /// </summary>
        [Required]
        public required string? Name { get; init; }

        /// <summary>
        /// The number of versions of this test that exist.
        /// </summary>
        [Required]
        public required int? NumberOfVersions { get; init; }

        /// <summary>
        /// The standardization factor
        /// </summary>
        [Required]
        public required int? StandardizationFactor { get; init; }

        /// <summary>
        /// The minimum grade
        /// </summary>
        [Required]
        public required int? MinimumGrade { get; init; }
    }
}