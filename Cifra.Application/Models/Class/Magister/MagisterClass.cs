using System.Collections.Generic;

namespace Cifra.Application.Models.Class.Magister
{
    /// <summary>
    /// A class from Magister
    /// </summary>
    public class MagisterClass
    {
        /// <summary>
        /// The name of the class
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The students of the class
        /// </summary>
        public IEnumerable<MagisterStudent> Students { get; set; }
    }
}
