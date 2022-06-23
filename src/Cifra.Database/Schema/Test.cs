using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cifra.Database.Schema
{
    public class Test
    {
        [Key]
        /// <summary>
        /// The Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The number of versions of the test that where made.
        /// </summary>
        [Required]
        public int NumberOfVersions { get; set; }

        /// <summary>
        /// The Assignments.
        /// </summary>
        public virtual ICollection<Assignment> Assignments { get; set; }

        /// <summary>
        /// The Standardization Factor.
        /// </summary>
        [Required]

        public int StandardizationFactor { get; set; }

        /// <summary>
        /// The Minimum Grade.
        /// </summary>
        [Required]
        public int MinimumGrade { get; set; }
    }
}
