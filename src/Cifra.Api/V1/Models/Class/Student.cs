namespace Cifra.Api.V1.Models.Class
{
    /// <summary>
    /// The Student entity.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// The id of the student
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The first name of the student
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// The infix of the student
        /// </summary>
        public string Infix { get; }

        /// <summary>
        /// The last name of the student
        /// </summary>
        public string LastName { get; }
    }
}
