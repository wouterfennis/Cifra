
namespace SpreadsheetWriter.Abstractions.File
{
    /// <summary>
    /// Factory for creating ISpreadsheetFile
    /// </summary>
    public interface ISpreadsheetFileFactory
    {
        /// <summary>
        /// Creates a ISpreadsheetFile
        /// </summary>
        /// <param name="directoryPath">The directory where the file should be saved</param>
        /// <param name="metadata">The metadata of the spreadsheet</param>
        /// <returns>ISpreadsheetFile</returns>
        ISpreadsheetFile Create(string directoryPath, Metadata metadata);
    }
}
