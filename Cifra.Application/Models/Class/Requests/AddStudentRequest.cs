using System;

namespace Cifra.Application.Models.Class.Requests
{
    /// <summary>
    /// The request to add an student
    /// </summary>
    public sealed class AddStudentRequest
    {
        /// <summary>
        /// The Id of the class
        /// </summary>
        public Guid ClassId { get; set; }

        /// <summary>
        /// The fullname of the student
        /// </summary>
        public string FullName { get; set; }
    }
}
