using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Test
{
    /// <summary>
    /// The Test entity.
    /// </summary>
    public sealed class Test
    {
        /// <summary>
        /// The Id.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// The Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of versions of the test that where made.
        /// </summary>
        public int NumberOfVersions { get; set; }

        /// <summary>
        /// The Assignments.
        /// </summary>
        public List<Assignment> Assignments { get; set; }

        /// <summary>
        /// The Standardization Factor.
        /// </summary>
        public int StandardizationFactor { get; set; }

        /// <summary>
        /// The Minimum Grade.
        /// </summary>
        public int MinimumGrade { get; set; }

    }
}
