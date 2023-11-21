using Cifra.Commands.Models;

namespace Cifra.Commands.Models
{
    /// <summary>
    /// The Class entity.
    /// </summary>
    public record Class
    {
        /// <summary>
        /// The Id of the Class
        /// </summary>
        public required uint Id { get; init; }

        /// <summary>
        /// The Name of the Class
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// The Students of the Class
        /// </summary>
        public required IEnumerable<Student> Students { get; init; }
    }
}
