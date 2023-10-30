
namespace Cifra.Api.V1.Models.Class.Responses
{
    /// <summary>
    /// The response to get a Class.
    /// </summary>
    public class GetClassResponse
    {
        /// <summary>
        /// The class
        /// </summary>
        public required Class RetrievedClass { get; init; }
    }
}
