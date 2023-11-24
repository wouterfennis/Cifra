using Cifra.Commands.Models;

namespace Cifra.Commands
{
    /// <summary>
    /// The command to update an Class
    /// </summary>
    public sealed record DeleteClassCommand
    {
        /// <summary>
        /// The name of the class that should be deleted
        /// </summary>
        public required string Name { get; init; }
    }
}
