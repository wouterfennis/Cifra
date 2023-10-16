
using Cifra.Domain.Spreadsheet;

namespace Cifra.Application.Models.Commands
{
    /// <summary>
    /// The command to create an test results spreadsheet.
    /// </summary>
    public sealed class CreateTestResultsSpreadsheetCommand
    {
        /// <summary>
        /// The id of the class.
        /// </summary>
        public int ClassId { get; init; }

        /// <summary>
        /// The id of the test.
        /// </summary>
        public int TestId { get; init; }

        /// <summary>
        /// The metadata of the spreadsheet.
        /// </summary>
        public Metadata Metadata { get; init; }
    }
}
