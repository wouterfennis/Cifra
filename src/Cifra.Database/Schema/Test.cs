using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cifra.Database.Schema
{
    public class Test
    {
        /// <summary>
        /// The Id.
        /// </summary>
        [Key]

        public Guid Id { get; }

        /// <summary>
        /// The Name.
        /// </summary>
        [Required]
        public string Name { get; }

        /// <summary>
        /// The number of versions of the test that where made.
        /// </summary>
        [Required]
        public int NumberOfVersions { get; }

        /// <summary>
        /// The Assignments.
        /// </summary>
        public List<Assignment> Assignments { get; }

        /// <summary>
        /// The Standardization Factor.
        /// </summary>
        [Required]

        public int StandardizationFactor { get; }

        /// <summary>
        /// The Minimum Grade.
        /// </summary>
        [Required]
        public int MinimumGrade { get; }
    }
}
