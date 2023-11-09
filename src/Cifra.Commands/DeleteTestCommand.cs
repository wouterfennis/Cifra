namespace Cifra.Commands
{
    /// <summary>
    /// The command to delete an test.
    /// </summary>
    public sealed record DeleteTestCommand
    {
        /// <summary>
        /// The id of the test that should be deleted.
        /// </summary>
        public required uint TestId { get; init; }
    }
}