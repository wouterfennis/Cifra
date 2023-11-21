
namespace Cifra.Api.IntegrationTests.Models
{
    internal class StudentModel
    {
        /// <summary>
        /// The number of questions of the assignment.
        /// </summary>
        public int? Id { get; init; }

        /// <summary>
        /// The first name of the student
        /// </summary>
        public string? FirstName { get; init; }

        /// <summary>
        /// The infix of the student
        /// </summary>
        public string? Infix { get; init; }

        /// <summary>
        /// The last name of the student
        /// </summary>
        public string? LastName { get; init; }
    }
}
