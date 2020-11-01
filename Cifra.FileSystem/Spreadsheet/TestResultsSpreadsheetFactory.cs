using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.FileSystem.Mapping;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.EPPlus;
using System.Drawing;
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

            var spreadsheetWriter = fileBuilder.CreateSpreadsheetWriter(metadata.FileName);
            int questionNamesColumns = test.GetMaximalQuestionNamesPerQuestion();

            spreadsheetWriter
                .Write(test.Name.Value)
                .MoveRightTimes(2)
                .Write(metadata.Created.ToString())
                .NewLine()
                .MoveRightTimes(questionNamesColumns)
                .Write("Naam");
            var studentNamesRowStartpoint = new Point(spreadsheetWriter.CurrentPosition.X + 1, spreadsheetWriter.CurrentPosition.Y);

            spreadsheetWriter
                .NewLine()
                .Write("Opgave")
                .MoveRightTimes(questionNamesColumns)
                .Write("Punten")
                .NewLine();
            var questionNamesColumnTopLeft = spreadsheetWriter.CurrentPosition;

            foreach (Question question in test.Questions)
            {
                foreach (var questionName in question.QuestionNames)
                {
                    spreadsheetWriter.Write(questionName.Value)
                        .MoveRight();
                }
                spreadsheetWriter.Write(question.MaximalScore.Value)
                    .NewLine();
            }

            spreadsheetWriter.Write("Totaal:")
                .MoveRight();
            var maximumPointsColumnTop = new Point(questionNamesColumnTopLeft.X + questionNamesColumns, questionNamesColumnTopLeft.Y);
            var maximumPointsColumnBottom = new Point(spreadsheetWriter.CurrentPosition.X, spreadsheetWriter.CurrentPosition.Y - 1);
            spreadsheetWriter.PlaceFormula(maximumPointsColumnTop, maximumPointsColumnBottom, FormulaType.SUM)
                .NewLine()
                .Write("Cijfer");
            // Build grade formula here

            spreadsheetWriter.CurrentPosition = studentNamesRowStartpoint;
            foreach (Student student in @class.Students)
            {
                Point studentColumnTop = spreadsheetWriter.CurrentPosition;
                spreadsheetWriter
                    .Write(student.FullName.Value);

                var scoredPointsColumnStart = new Point(spreadsheetWriter.CurrentPosition.X, maximumPointsColumnTop.Y);
                var scoredPointsColumnEnd = new Point(spreadsheetWriter.CurrentPosition.X, maximumPointsColumnBottom.Y);

                spreadsheetWriter.CurrentPosition = scoredPointsColumnEnd;
                spreadsheetWriter.MoveDown()
                    .PlaceFormula(scoredPointsColumnStart, scoredPointsColumnEnd, FormulaType.SUM);

                spreadsheetWriter.CurrentPosition = studentColumnTop;
                spreadsheetWriter.MoveRight();
            }

            fileBuilder.FillMetadata(metadata.MapToLibraryModel());

            await fileBuilder.SaveAsync();
        }
    }
}
