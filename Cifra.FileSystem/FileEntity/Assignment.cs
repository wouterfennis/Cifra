using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.FileSystem.FileEntity
{
    internal sealed class Assignment
    {
        public Guid Id { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}
