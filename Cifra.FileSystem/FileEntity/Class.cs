using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.FileSystem.FileEntity
{
    internal sealed class Class
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
