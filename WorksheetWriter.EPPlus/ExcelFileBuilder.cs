using OfficeOpenXml;
using System.IO;
using System.Threading.Tasks;

namespace SpreadsheetWriter.EPPlus
{
    public sealed class ExcelFileBuilder
    {
        private readonly ExcelPackage _excelPackage;

        public ExcelFileBuilder(FileInfo fileInfo)
        {
            _excelPackage = new ExcelPackage(fileInfo);
        }

        public void FillMetadata(Metadata metadata)
        {
            _excelPackage.Workbook.Properties.Author = metadata.Author;
            _excelPackage.Workbook.Properties.Title = metadata.Title;
            _excelPackage.Workbook.Properties.Subject = metadata.Subject;
            _excelPackage.Workbook.Properties.Created = metadata.Created;
        }

        public ExcelSpreadsheetWriter CreateSpreadsheetWriter(string name)
        {
            ExcelWorksheet worksheet = _excelPackage.Workbook.Worksheets.Add(name);
            return new ExcelSpreadsheetWriter(worksheet);
        }

        public async Task SaveAsync()
        {
            await _excelPackage.SaveAsync();
        }

        public void Dispose()
        {
            _excelPackage.Dispose();
        }
    }
}
