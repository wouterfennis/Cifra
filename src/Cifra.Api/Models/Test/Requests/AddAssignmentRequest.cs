using System;

namespace Cifra.Api.Models.Test.Requests
{
    /// <summary>
    /// The request to add an assignment
    /// </summary>
    public sealed class AddAssignmentRequest
    {
        /// <summary>
        /// The Test Id where the assignment should be added
        /// </summary>
        public Guid TestId { get; set; }

        /// <summary>
        /// The number of questions in this Assignment.
        /// </summary>
        public int NumberOfQuestions { get; set; }
    }
}
