using System.Collections.Generic;

namespace Cifra.Application.Models.Class.Magister
{
    public class MagisterClass
    {
        public string Name { get; set; }
        public IEnumerable<MagisterStudent> Students { get; set; }
    }
}
