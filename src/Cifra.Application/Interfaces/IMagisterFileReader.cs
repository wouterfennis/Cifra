
using Cifra.Application.Models.Class.Magister;
using Cifra.Application.Models.ValueTypes;

namespace Cifra.Application.Interfaces
{
    /// <summary>
    /// Reader for files from Magister
    /// </summary>
    public interface IMagisterFileReader
    {
        /// <summary>
        /// Reads a file and converts the data to MagisterClass
        /// </summary>
        MagisterClass ReadClass(Path fileLocation);
    }
}