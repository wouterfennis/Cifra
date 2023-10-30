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
        public required string Name { get; init; }

        /// <summary>
        /// The number of versions of this test that exist.
        /// </summary>
        public required int NumberOfVersions { get; init; }

        /// <summary>
        /// The standardization factor
        /// </summary>
        public required int StandardizationFactor { get; init; }

        /// <summary>
        /// The minimum grade
        /// </summary>
        public required int MinimumGrade { get; init; }
    }
}