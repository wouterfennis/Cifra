using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Models.Test.Requests
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
    }
}
