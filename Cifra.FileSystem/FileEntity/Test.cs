using System;
using System.Collections.Generic;

namespace Cifra.FileSystem.FileEntity
{
    /// <summary>
    /// The test entity.
    /// </summary>
    public sealed class Test
    {
        /// <summary>
        /// The id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the <see cref="Test"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The assignments of the <see cref="Test"/>.
        /// </summary>
        public IEnumerable<Assignment> Assignments { get; set; }

        /// <summary>
        /// The minimum grade of the <see cref="Test"/>.
        /// </summary>
        public byte MinimumGrade { get; set; }

        /// <summary>
        /// The standardization factor of the <see cref="Test"/>.
        /// </summary>
        public byte StandardizationFactor { get; set; }

        /// <summary>
        /// The number of versions that exist of the <see cref="Test"/>.
        /// </summary>
        public int NumberOfVersions { get; set; }
    }
}
