using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Class.Requests
{
    /// <summary>
    /// The request to add an student
    /// </summary>
    public sealed class AddStudentsRequest
    {
        public IEnumerable<StudentDto> Students { get; set; }
    }

    public sealed class StudentDto
    {
        /// <summary>
        /// The first name of the student
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The infix of the student
        /// </summary>
        public string Infix { get; set; }

        /// <summary>
        /// The last name of the student
        /// </summary>
        public string LastName { get; set; }
    }
}
