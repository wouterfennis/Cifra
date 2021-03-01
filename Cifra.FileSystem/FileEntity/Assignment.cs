using System;

namespace Cifra.FileSystem.FileEntity
{
    /// <summary>
    /// The assignment entity.
    /// </summary>
    public sealed class Assignment
    {
        /// <summary>
        /// The id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The number of questions of the <see cref="Assignment"/>.
        /// </summary>
        public int NumberOfQuestions { get; set; }
    }
}
