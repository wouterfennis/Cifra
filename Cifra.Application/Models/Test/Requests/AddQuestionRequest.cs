using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Models.Test.Requests
{
    /// <summary>
    /// The request to add an question
    /// </summary>
    public sealed class AddQuestionRequest
    {
        /// <summary>
        /// The Test Id where the Question should be added
        /// </summary>
        public Guid TestId { get; set; }

        /// <summary>
        /// The Assignment Id where the Question should be added
        /// </summary>
        public Guid AssignmentId { get; set; }

        /// <summary>
        /// The names of the question
        /// </summary>
        public IEnumerable<string> Names { get; set; }

        /// <summary>
        /// The maximum score
        /// </summary>
        public byte MaximumScore { get; set; }
    }
}
