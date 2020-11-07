﻿using Cifra.Application.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.FileSystem.FileEntity
{
    internal sealed class Test
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
        public byte MinimumGrade { get; set; }
        public byte StandardizationFactor { get; set; }
    }
}
