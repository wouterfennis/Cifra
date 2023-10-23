using Cifra.Commands.Models;

namespace Cifra.Commands.Models
{
    /// <summary>
    /// The Class entity.
    /// </summary>
    public class Class
    {
        /// <summary>
        /// The Id of the Class
        /// </summary>
        public uint Id { get; init; }

        /// <summary>
        /// The Name of the Class
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// The Students of the Class
        /// </summary>
        public IEnumerable<Student> Students { get; init; }
    }
}
