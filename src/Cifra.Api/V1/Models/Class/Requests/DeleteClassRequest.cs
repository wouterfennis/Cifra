using System.ComponentModel.DataAnnotations;

namespace Cifra.Api.V1.Models.Class.Requests
{
    /// <summary>
    /// The request to delete a Class.
    /// </summary>
    public sealed class DeleteClassRequest
    {
        /// <summary>
        /// The name of the class.
        /// </summary>
        [Required]
        public required string? Name { get; init; }
    }
}
