namespace Cifra.Commands.Models
{
    /// <summary>
    /// The Test entity.
    /// </summary>
    public sealed record Test
    {
        /// <summary>
        /// The Id.
        /// </summary>
        public required uint Id { get; init; }

        /// <summary>
        /// The Name.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// The number of versions of the test that where made.
        /// </summary>
        public required int NumberOfVersions { get; init; }

        /// <summary>
        /// The Assignments.
        /// </summary>
        public required List<Assignment> Assignments { get; init; }

        /// <summary>
        /// The Standardization Factor.
        /// </summary>
        public required int StandardizationFactor { get; init; }

        /// <summary>
        /// The Minimum Grade.
        /// </summary>
        public required int MinimumGrade { get; init; }

    }
}
