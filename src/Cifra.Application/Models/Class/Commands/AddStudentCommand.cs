using System;

namespace Cifra.Application.Models.Class.Commands
{
    /// <summary>
    /// The request to add an student
    /// </summary>
    public sealed class AddStudentCommand
    {
        /// <summary>
        /// The Id of the class
        /// </summary>
        public Guid ClassId { get; set; }

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
