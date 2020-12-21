using OfficeOpenXml;
using SpreadsheetWriter.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SpreadsheetWriter.EPPlus
{
    public sealed class ExcelFileBuilder : ISpreadsheetFileBuilder
    {
        private ExcelPackage _excelPackage;

        public ISpreadsheetFileBuilder CreateNew(FileInfo fileInfo)
        {
            _excelPackage = new ExcelPackage(fileInfo);
            return this;
        }

        public void FillMetadata(Metadata metadata)
        {
            if(_excelPackage == null)
            {
                throw new InvalidOperationException($"No excel package has been made, Use {nameof(CreateNew)} to create one.");
            }
            _excelPackage.Workbook.Properties.Author = metadata.Author;
            _excelPackage.Workbook.Properties.Title = metadata.Title;
            _excelPackage.Workbook.Properties.Subject = metadata.Subject;
            _excelPackage.Workbook.Properties.Created = metadata.Created;
        }

        public ISpreadsheetWriter CreateSpreadsheetWriter(string name)
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
