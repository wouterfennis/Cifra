using System;
using System.Collections.Generic;

namespace Cifra.FileSystem.FileEntity
{
    public sealed class Test
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
        public byte MinimumGrade { get; set; }
        public byte StandardizationFactor { get; set; }
    }
}
