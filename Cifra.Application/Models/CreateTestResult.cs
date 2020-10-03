using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Models
{
    public class CreateTestResult
    {
        public Test Test { get; set; }

        public IEnumerable<ValidationMessage>? ValidationMessages { get; set; }
    }
}
