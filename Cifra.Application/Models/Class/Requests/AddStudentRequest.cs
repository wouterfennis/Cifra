using System;

namespace Cifra.Application.Models.Class.Requests
{
    public class AddStudentRequest
    {
        public Guid ClassId { get; set; }
        public string FullName { get; set; }
    }
}
