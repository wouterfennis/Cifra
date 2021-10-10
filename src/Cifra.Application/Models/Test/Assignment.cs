namespace Cifra.Application.Models.Test
{
    /// <summary>
    /// The Assignment entity.
    /// </summary>
    public sealed class Assignment
    {
        /// <summary>
        /// The id of the <see cref="Assignment"/>.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The number of questions the <see cref="Assignment"/> has.
        /// </summary>
        public int NumberOfQuestions { get; }

        /// <summary>
        /// Constructor for existing assignment.
        /// </summary>
        public Assignment(int id, int numberOfQuestions)
        {
            Id = id;
            NumberOfQuestions = numberOfQuestions;
        }

        /// <summary>
        /// Constructor for new assignment.
        /// </summary>
        public Assignment(int numberOfQuestions)
        {
            NumberOfQuestions = numberOfQuestions;
        }
    }
}