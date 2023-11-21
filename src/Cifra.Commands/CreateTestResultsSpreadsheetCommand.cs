
using Cifra.Commands.Models;

namespace Cifra.Commands
{
    /// <summary>
    /// The command to create an test results spreadsheet.
    /// </summary>
    public sealed record CreateTestResultsSpreadsheetCommand
    {
        /// <summary>
        /// The id of the class.
        /// </summary>
        public required uint ClassId { get; init; }

        /// <summary>
        /// The id of the test.
        /// </summary>
        public required uint TestId { get; init; }

        /// <summary>
        /// The metadata of the spreadsheet.
        /// </summary>
        public required Metadata Metadata { get; init; }
    }
}
