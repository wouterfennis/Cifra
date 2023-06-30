namespace Cifra.Application.Models.Test.Commands
{
    /// <summary>
    /// The request to update a test
    /// </summary>
    public sealed class UpdateTestCommand
    {
        /// <summary>
        /// The test
        /// </summary>
        public Domain.Test Test { get; set; }
    }
}