namespace Cifra.Commands
{
    /// <summary>
    /// The request to create an Class
    /// </summary>
    public sealed class CreateClassCommand
    {
        /// <summary>
        /// The name of the class
        /// </summary>
        public required string Name { get; init; }
    }
}
