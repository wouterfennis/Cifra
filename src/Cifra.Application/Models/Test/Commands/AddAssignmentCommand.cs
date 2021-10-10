namespace Cifra.Application.Models.Test.Commands
{
    /// <summary>
    /// The request to add an assignment
    /// </summary>
    public sealed class AddAssignmentCommand
    {
        /// <summary>
        /// The Test Id where the assignment should be added
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// The number of questions in this Assignment.
        /// </summary>
        public int NumberOfQuestions { get; set; }
    }
}
