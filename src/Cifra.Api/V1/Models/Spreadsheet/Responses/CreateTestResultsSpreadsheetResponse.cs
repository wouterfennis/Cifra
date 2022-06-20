using Cifra.Api.V1.Models.Validation;
using System.Collections.Generic;

namespace Cifra.Api.V1.Models.Spreadsheet.Responses
{
    public class CreateTestResultsSpreadsheetResponse
    {
        /// <summary>
        /// The path to the spreadsheet
        /// </summary>
        public string SpreadsheetPath { get; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }
    }
}
