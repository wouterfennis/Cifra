using System;
using System.Collections.Generic;

namespace Cifra.FileSystem.FileEntity
{
    /// <summary>
    /// The class entity.
    /// </summary>
    public sealed class Class
    {
        /// <summary>
        /// The id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the <see cref="Class"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The students of the <see cref="Class"/>.
        /// </summary>
        public IEnumerable<Student> Students { get; set; }
    }
}
