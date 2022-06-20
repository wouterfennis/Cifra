
using Cifra.Core.Models.Spreadsheet;

namespace Cifra.Application.Models.Spreadsheet.Commands
{
    /// <summary>
    /// The request to create an test results spreadsheet.
    /// </summary>
    public sealed class CreateTestResultsSpreadsheetCommand
    {
        /// <summary>
        /// The id of the class.
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// The id of the test.
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// The metadata of the spreadsheet.
        /// </summary>
        public Metadata Metadata { get; set; }
    }
}
