
namespace Cifra.Api.IntegrationTests.Models
{
    internal class StudentModel
    {
        /// <summary>
        /// The number of questions of the assignment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The first name of the student
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The infix of the student
        /// </summary>
        public string? Infix { get; set; }

        /// <summary>
        /// The last name of the student
        /// </summary>
        public string LastName { get; set; }
    }
}
