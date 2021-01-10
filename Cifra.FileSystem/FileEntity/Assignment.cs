using System;
using System.Collections.Generic;

namespace Cifra.FileSystem.FileEntity
{
    public sealed class Assignment
    {
        public Guid Id { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}
