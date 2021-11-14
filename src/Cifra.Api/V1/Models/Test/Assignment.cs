using System;

namespace Cifra.Api.V1.Models.Test
{
    /// <summary>
    /// The Assignment entity.
    /// </summary>
    public sealed class Assignment
    {
        /// <summary>
        /// The id of the <see cref="Assignment"/>.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The number of questions the <see cref="Assignment"/> has.
        /// </summary>
        public int NumberOfQuestions { get; set; }
    }
}