using System;
using System.Collections.Generic;

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
        /// The questions of the <see cref="Assignment"/>.
        /// </summary>
        public IEnumerable<Question> Questions { get; set; }
    }
}
