using System.Collections.Generic;

namespace Cifra.Application.Models
{
    /// <summary>
    /// The Test entity.
    /// </summary>
    public sealed class Test
    {
        /// <summary>
        /// The Id.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// The Name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// The number of versions of the test that where made.
        /// </summary>
        public int NumberOfVersions { get; init; }

        /// <summary>
        /// The Assignments.
        /// </summary>
        public List<Assignment> Assignments { get; init; }

        /// <summary>
        /// The Standardization Factor.
        /// </summary>
        public int StandardizationFactor { get; init; }

        /// <summary>
        /// The Minimum Grade.
        /// </summary>
        public int MinimumGrade { get; init; }

    }
}
