using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Class
{
    /// <summary>
    /// The Class entity.
    /// </summary>
    public class Class
    {
        /// <summary>
        /// The Id of the Class
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the Class
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Students of the Class
        /// </summary>
        public IEnumerable<Student> Students { get; set; }
    }
}
