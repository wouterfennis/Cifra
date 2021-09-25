using System;

namespace Cifra.Api.Models.Test.Requests
{
    /// <summary>
    /// The request to add an assignment
    /// </summary>
    public sealed class AddBonusAssignmentRequest
    {
        /// <summary>
        /// The Test Id where the assignment should be added
        /// </summary>
        public Guid TestId { get; set; }
    }
}
