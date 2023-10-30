using Cifra.Api.V1.Models.Validation;
using System.Collections.Generic;
using System.IO;

namespace Cifra.Api.V1.Models.Spreadsheet.Responses
{
    /// <summary>
    /// The response to create a test results spreadsheet.
    /// </summary>
    public class CreateTestResultsSpreadsheetResponse
    {
        /// <summary>
        /// The path to the spreadsheet.
        /// </summary>
        public required FileInfo FileInfo { get; init; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public required IEnumerable<ValidationMessage> ValidationMessages { get; init; }
    }
}
