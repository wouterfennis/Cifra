using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Models
{
    internal class Class
    {
        public Name ClassName { get; }
        public IEnumerable<Student> Collection { get; }

        public Class(Name className, IEnumerable<Student> collection)
        {
            ClassName = className;
            Collection = collection;
        }
    }
}
