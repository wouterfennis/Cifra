
namespace Cifra.Api.IntegrationTests.Models
{
    internal class TestModel
    {
        /// <summary>
        /// The Name.
        /// </summary>
        public string? Name { get; init; } = DefaultValues.DefaultTestName;

        /// <summary>
        /// The number of versions of the test that where made.
        /// </summary>
        public int? NumberOfVersions { get; init; } = DefaultValues.NumberOfVersions;

        /// <summary>
        /// The Standardization Factor.
        /// </summary>

        public int? StandardizationFactor { get; init; } = DefaultValues.StandardizationFactor;

        /// <summary>
        /// The Minimum Grade.
        /// </summary>
        public int? MinimumGrade { get; init; } = DefaultValues.MinimumGrade;
    }
}
