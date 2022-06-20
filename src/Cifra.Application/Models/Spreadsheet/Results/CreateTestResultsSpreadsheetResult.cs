using Cifra.Core.Models.Validation;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Spreadsheet.Results
{
    /// <summary>
    /// Result of the Create Class operation
    /// </summary>
    public sealed class CreateTestResultsSpreadsheetResult
    {
        /// <summary>
        /// The path to the spreadsheet.
        /// </summary>
        public string SpreadsheetPath { get; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        internal CreateTestResultsSpreadsheetResult(string spreadsheetPath)
        {
            SpreadsheetPath = spreadsheetPath;
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        internal CreateTestResultsSpreadsheetResult(IEnumerable<ValidationMessage> validationMessages)
        {
            ValidationMessages = validationMessages ?? throw new ArgumentNullException(nameof(validationMessages));
        }
    }
}
