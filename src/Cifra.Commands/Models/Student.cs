namespace Cifra.Commands.Models
{
    /// <summary>
    /// The Student entity.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// The id of the student
        /// </summary>
        public uint Id { get; init; }

        /// <summary>
        /// The first name of the student
        /// </summary>
        public string FirstName { get; init; }

        /// <summary>
        /// The infix of the student
        /// </summary>
        public string? Infix { get; init; }

        /// <summary>
        /// The last name of the student
        /// </summary>
        public string LastName { get; init; }
    }
}
