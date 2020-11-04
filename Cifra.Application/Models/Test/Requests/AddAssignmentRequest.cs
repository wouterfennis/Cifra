using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Models.Test.Requests
{
    public sealed class AddAssignmentRequest
    {
        public Guid TestId { get; set; }
        public string Name { get; set; }
    }
}
