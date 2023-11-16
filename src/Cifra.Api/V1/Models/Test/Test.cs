using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public required uint? Id { get; init; }

        /// <summary>
        /// The Name.
        /// </summary>
        [Required]
        public required string? Name { get; init; }

        /// <summary>
        /// The number of versions of the test that where made.
        /// </summary>
        [Required]
        public required int? NumberOfVersions { get; init; }

        /// <summary>
        /// The Assignments.
        /// </summary>
        public required List<Assignment> Assignments { get; init; }

        /// <summary>
        /// The Standardization Factor.
        /// </summary>
        [Required]
        public required int? StandardizationFactor { get; init; }

        /// <summary>
        /// The Minimum Grade.
        /// </summary>
        [Required]
        public required int? MinimumGrade { get; init; }

    }
}
