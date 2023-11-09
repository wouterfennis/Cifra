namespace Cifra.Api.V1.Models.Spreadsheet.Requests
{
    /// <summary>
    /// The request to create a Class.
    /// </summary>
    public sealed class CreateTestResultsSpreadsheetRequest
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
        public required Metadata Metadata { get; init; }
    }
}
