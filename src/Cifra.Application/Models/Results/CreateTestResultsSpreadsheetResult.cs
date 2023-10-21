using Cifra.Domain.Validation;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cifra.Application.Models.Results
{
    /// <summary>
    /// Result of the Create Class operation
    /// </summary>
    public sealed class CreateTestResultsSpreadsheetResult
    {
        /// <summary>
        /// The path to the spreadsheet.
        /// </summary>
        public FileInfo FileInfo { get; }

        /// <summary>
        /// The validation messages
        /// </summary>
        public IEnumerable<ValidationMessage> ValidationMessages { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        internal CreateTestResultsSpreadsheetResult(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
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
