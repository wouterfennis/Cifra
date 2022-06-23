using System.ComponentModel.DataAnnotations;

namespace Cifra.Database.Schema
{
    /// <summary>
    /// The Student entity
    /// </summary>
    public class Student
    {
        [Key]
        /// <summary>
        /// The id of the student
        /// </summary>
        public int Id { get; set; }

        [Required]
        /// <summary>
        /// The first name of the student
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The infix of the student
        /// </summary>
        public string Infix { get; set; }

        [Required]
        /// <summary>
        /// The last name of the student
        /// </summary>
        public string LastName { get; set; }
    }
}
