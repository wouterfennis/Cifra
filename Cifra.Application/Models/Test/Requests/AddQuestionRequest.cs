using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Models.Test.Requests
{
    public sealed class AddQuestionRequest
    {
        public Guid TestId { get; set; }
        public Guid AssignmentId { get; set; }
        public IEnumerable<string> Names { get; set; }
        public byte MaximalScore { get; set; }
    }
}
