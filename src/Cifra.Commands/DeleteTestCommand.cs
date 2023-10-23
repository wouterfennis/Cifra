namespace Cifra.Commands
{
    /// <summary>
    /// The command to delete an test.
    /// </summary>
    public sealed class DeleteTestCommand
    {
        /// <summary>
        /// The id of the test that should be deleted.
        /// </summary>
        public uint TestId { get; init; }
    }
}