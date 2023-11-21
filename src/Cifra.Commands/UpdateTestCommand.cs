using Cifra.Commands.Models;

namespace Cifra.Commands
{
    /// <summary>
    /// The request to update a test
    /// </summary>
    public sealed record UpdateTestCommand
    {
        /// <summary>
        /// The updated test
        /// </summary>
        public required Test Test { get; init; }
    }
}
