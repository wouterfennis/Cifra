﻿using OfficeOpenXml;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.File;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SpreadsheetWriter.EPPlus.File
{
    public sealed class ExcelFile : ISpreadsheetFile
    {
        private ExcelPackage _excelPackage;
        private ISpreadsheetWriter _writer;

        public ExcelFile(string fileLocation, Metadata metadata)
        {
            var fileInfo = new FileInfo(fileLocation);
            _excelPackage = new ExcelPackage(fileInfo);
            _excelPackage.Workbook.Properties.Author = metadata.Author;
            _excelPackage.Workbook.Properties.Title = metadata.Title;
            _excelPackage.Workbook.Properties.Subject = metadata.Subject;
            _excelPackage.Workbook.Properties.Created = metadata.Created;

            ExcelWorksheet worksheet = _excelPackage.Workbook.Worksheets.Add(metadata.Title);
            _writer = new ExcelSpreadsheetWriter(worksheet);
        }

        public ISpreadsheetWriter GetSpreadsheetWriter()
        {
            return _writer;
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