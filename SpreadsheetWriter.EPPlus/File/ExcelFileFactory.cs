
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.File;

namespace SpreadsheetWriter.EPPlus.File
{
    public class ExcelFileFactory : ISpreadsheetFileFactory
    {
        public ISpreadsheetFile Create(string directoryPath, Metadata metadata)
        {
            string filePath = System.IO.Path.Combine(directoryPath, $"{metadata.FileName}.xlsx");
            return new ExcelFile(filePath, metadata);
        }
    }
}
