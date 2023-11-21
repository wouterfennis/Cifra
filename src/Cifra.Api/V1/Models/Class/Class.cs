using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cifra.Api.V1.Models.Class
{
    /// <summary>
    /// The Class entity.
    /// </summary>
    public class Class
    {
        /// <summary>
        /// The Id of the Class
        /// </summary>
        public required uint? Id { get; init; }

        /// <summary>
        /// The Name of the Class
        /// </summary>
        [Required]
        public required string? Name { get; init; }

        /// <summary>
        /// The Students of the Class
        /// </summary>
        public required IEnumerable<Student> Students { get; init; }
    }
}
