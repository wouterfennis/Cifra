
namespace Cifra.Application.Models.Class.Commands
{
    /// <summary>
    /// The request to update an Class
    /// </summary>
    public sealed class UpdateClassCommand
    {
        /// <summary>
        /// The updated class.
        /// </summary>
        public Domain.Class UpdatedClass { get; set; }
    }
}
