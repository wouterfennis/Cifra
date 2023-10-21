
using Cifra.Commands.Models;

namespace Cifra.Commands
{
    /// <summary>
    /// The command to create an test results spreadsheet.
    /// </summary>
    public sealed class CreateTestResultsSpreadsheetCommand
    {
        /// <summary>
        /// The id of the class.
        /// </summary>
        public uint ClassId { get; init; }

        /// <summary>
        /// The id of the test.
        /// </summary>
        public uint TestId { get; init; }

        /// <summary>
        /// The metadata of the spreadsheet.
        /// </summary>
        public Metadata Metadata { get; init; }
    }
}
