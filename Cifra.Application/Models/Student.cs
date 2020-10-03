using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Cifra.Application.Models
{
    class Student
    {
        public Name FullName { get; }

        public Student(Name fullName)
        {
            FullName = fullName;
        }
    }
}
