﻿using System.ComponentModel.DataAnnotations;

namespace Cifra.Api.V1.Models.Class
{
    /// <summary>
    /// The Student entity.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// The id of the student
        /// </summary>
        public required uint? Id { get; init; }

        /// <summary>
        /// The first name of the student
        /// </summary>
        [Required]
        public required string? FirstName { get; init; }

        /// <summary>
        /// The infix of the student
        /// </summary>
        public required string? Infix { get; init; }

        /// <summary>
        /// The last name of the student
        /// </summary>
        [Required]
        public required string? LastName { get; init; }
    }
}
