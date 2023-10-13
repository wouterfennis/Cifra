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
