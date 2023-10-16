using System;

namespace Cifra.Application.Models
{
    /// <summary>
    /// The Assignment entity.
    /// </summary>
    public sealed class Assignment
    {
        /// <summary>
        /// The id of the <see cref="Assignment"/>.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// The number of questions the <see cref="Assignment"/> has.
        /// </summary>
        public int NumberOfQuestions { get; init; }
    }
}