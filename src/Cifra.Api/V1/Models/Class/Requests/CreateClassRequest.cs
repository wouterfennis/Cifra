namespace Cifra.Api.V1.Models.Class.Requests
{
    /// <summary>
    /// The request to create a Class.
    /// </summary>
    public sealed class CreateClassRequest
    {
        /// <summary>
        /// The name of the class.
        /// </summary>
        public required string Name { get; init; }
    }
}
