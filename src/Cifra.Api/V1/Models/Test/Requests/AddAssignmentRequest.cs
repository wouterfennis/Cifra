namespace Cifra.Api.V1.Models.Test.Requests
{
    /// <summary>
    /// The request to add an assignment
    /// </summary>
    public sealed class AddAssignmentRequest
    {
        /// <summary>
        /// The number of questions in this Assignment.
        /// </summary>
        public int NumberOfQuestions { get; set; }
    }
}
