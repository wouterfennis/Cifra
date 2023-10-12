namespace Cifra.Application.Models.Test.Commands
{
    /// <summary>
    /// The request to delete an test.
    /// </summary>
    public sealed class DeleteTestCommand
    {
        /// <summary>
        /// The id of the test that should be deleted.
        /// </summary>
        public int TestId { get; set; }
    }
}