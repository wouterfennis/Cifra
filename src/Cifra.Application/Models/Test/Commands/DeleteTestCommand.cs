namespace Cifra.Application.Models.Test.Commands
{
    /// <summary>
    /// The request to delete an test.
    /// </summary>
    public sealed class DeleteTestCommand
    {
        /// <summary>
        /// The name of the test that should be deleted.
        /// </summary>
        public string Name { get; set; }
    }
}