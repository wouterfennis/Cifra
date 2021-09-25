
namespace Cifra.Application.Models.Class.Commands
{
    /// <summary>
    /// The request to create an Class with Magister
    /// </summary>
    public sealed class CreateMagisterClassCommand
    {
        /// <summary>
        /// The file location
        /// </summary>
        public string MagisterFileLocation { get; set; }
    }
}
