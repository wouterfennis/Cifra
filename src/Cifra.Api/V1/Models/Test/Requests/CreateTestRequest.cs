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
        public string Name { get; init; }

        /// <summary>
        /// The number of versions of this test that exist.
        /// </summary>
        public int NumberOfVersions { get; init; }

        /// <summary>
        /// The standardization factor
        /// </summary>
        public int StandardizationFactor { get; init; }

        /// <summary>
        /// The minimum grade
        /// </summary>
        public int MinimumGrade { get; init; }
    }
}