
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.File;

namespace SpreadsheetWriter.EPPlus.File
{
    /// <inheritdoc/>
    public class ExcelFileFactory : ISpreadsheetFileFactory
    {
        /// <inheritdoc/>
        public ISpreadsheetFile Create(string directoryPath, Metadata metadata)
        {
            string filePath = System.IO.Path.Combine(directoryPath, $"{metadata.FileName}.xlsx");
            return new ExcelFile(filePath, metadata);
        }
    }
}
