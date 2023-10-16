namespace Cifra.Application.Models.Commands
{
    /// <summary>
    /// The request to update a test
    /// </summary>
    public sealed class UpdateTestCommand
    {
        /// <summary>
        /// The updated test
        /// </summary>
        public Test Test { get; init; }
    }
}
