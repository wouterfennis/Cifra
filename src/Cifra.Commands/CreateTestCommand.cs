namespace Cifra.Commands
{
    /// <summary>
    /// The command to create an test
    /// </summary>
    public sealed class CreateTestCommand
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