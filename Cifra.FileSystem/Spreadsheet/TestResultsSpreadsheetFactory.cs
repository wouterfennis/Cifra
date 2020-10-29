using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.FileSystem.Mapping;
using SpreadsheetWriter.EPPlus;
using System.IO;
using System.Threading.Tasks;

namespace Cifra.FileSystem.Spreadsheet
{
    public class TestResultsSpreadsheetFactory : ITestResultsSpreadsheetFactory
    {
        private readonly IDirectoryInfoWrapper _spreadsheetDirectory;

        public TestResultsSpreadsheetFactory(IFileLocationProvider locationProvider)
        {
            _spreadsheetDirectory = locationProvider.GetSpreadsheetDirectoryPath();
        }

        public async Task CreateTestResultsSpreadsheetAsync(Class @class, Test test, Application.Models.Spreadsheet.Metadata metadata)
        {
            var newFilePath = System.IO.Path.Combine(_spreadsheetDirectory.FullName, $"{metadata.FileName}.xlsx");
            FileInfo newFile = new FileInfo(newFilePath);
            var fileBuilder = new ExcelFileBuilder(newFile);

            var spreadSheetWriter = fileBuilder.CreateSpreadsheetWriter(metadata.FileName);
            spreadSheetWriter
                .Write(metadata.FileName)
                .MoveRight()
                .Write(metadata.Created.ToString())
                .NewLine()
                .Write(test.Name.Value)
                .NewLine()
                .NewLine();

            foreach (Question question in test.Questions)
            {
                //  question.QuestionNames
            }
            fileBuilder.FillMetadata(metadata.MapToLibraryModel());

            await fileBuilder.SaveAsync();
        }
    }
}
