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
