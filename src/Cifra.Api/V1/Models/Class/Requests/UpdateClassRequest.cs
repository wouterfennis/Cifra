namespace Cifra.Api.V1.Models.Class.Requests
{
    /// <summary>
    /// The request to update a Class.
    /// </summary>
    public sealed class UpdateClassRequest
    {
        /// <summary>
        /// The updated class.
        /// </summary>
        public required Class UpdatedClass { get; init; }
    }
}
