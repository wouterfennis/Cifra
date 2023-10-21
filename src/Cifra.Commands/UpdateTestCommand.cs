using Cifra.Commands.Models;

namespace Cifra.Commands
{
    /// <summary>
    /// The request to update a test
    /// </summary>
    public sealed class UpdateTestCommand
    {
        /// <summary>
        /// The updated test
        /// </summary>
        public Test Test { get; init; }
    }
}
