using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using SpreadsheetWriter.EPPlus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cifra.FileSystem.Spreadsheet
{
    public class TestResultsSpreadsheetFactory
    {
        private readonly IDirectoryInfoWrapper _spreadsheetDirectory;

        public TestResultsSpreadsheetFactory(IFileLocationProvider locationProvider)
        {
            _spreadsheetDirectory = locationProvider.GetSpreadsheetDirectoryPath();
        }

        public void CreateTestResultsSpreadsheet(Class @class, Test test, Name fileName)
        {
            var newFilePath = System.IO.Path.Combine(_spreadsheetDirectory.FullName, fileName.Value);
            FileInfo newFile = new FileInfo(newFilePath);
            var fileBuilder = new ExcelFileBuilder(newFile);

            var spreadSheetWriter = fileBuilder.CreateSpreadsheetWriter(fileName.Value);

            spreadSheetWriter;

            FillMetadata();
        }

        private void FillMetadata(ExcelFileBuilder fileBuilder)
        {
            var metadata = new Metadata
            {
                Title = "Test results",
                Subject = "todo",
                Author = "todo",
                Created = DateTime.Now,
            };
            fileBuilder.FillMetadata();
        }
    }
}
