using System.ComponentModel.DataAnnotations;

namespace Cifra.Database.Schema
{
    /// <summary>
    /// The Assignment entity.
    /// </summary>
    public class Assignment
    {
        /// <summary>
        /// The id of the <see cref="Assignment"/>.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The number of questions the <see cref="Assignment"/> has.
        /// </summary>
        [Required]
        public int NumberOfQuestions { get; set; }
    }
}