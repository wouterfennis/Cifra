namespace Cifra.Commands
{
    /// <summary>
    /// The command to create an test
    /// </summary>
    public sealed record CreateTestCommand
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