using System;

namespace Cifra.Application.Models.Test.Commands
{
    /// <summary>
    /// The request to add an assignment
    /// </summary>
    public sealed class AddBonusAssignmentCommand
    {
        /// <summary>
        /// The Test Id where the assignment should be added
        /// </summary>
        public Guid TestId { get; set; }
    }
}
