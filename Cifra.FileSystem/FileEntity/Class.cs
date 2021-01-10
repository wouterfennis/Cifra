using System;
using System.Collections.Generic;

namespace Cifra.FileSystem.FileEntity
{
    public sealed class Class
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
