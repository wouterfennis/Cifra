using System;

namespace Cifra.Application.Models.Class.Requests
{
    public sealed class AddStudentRequest
    {
        public Guid ClassId { get; set; }
        public string FullName { get; set; }
    }
}
