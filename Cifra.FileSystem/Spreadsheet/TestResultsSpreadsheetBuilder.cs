using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Mapping;
using Cifra.FileSystem.Spreadsheet.Blocks;
using OfficeOpenXml;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.EPPlus;
using System.Drawing;
using System.Threading.Tasks;

namespace Cifra.FileSystem.Spreadsheet
{
    public class TestResultsSpreadsheetBuilder : ITestResultsSpreadsheetFactory
    {
        private readonly Path _spreadsheetDirectory;
        private readonly Color _tableAssignmentRowColor = Color.FromArgb(217, 225, 242);
        private readonly IDirectoryInfoWrapperFactory _directoryInfoWrapperFactory;
        private readonly IFileInfoWrapperFactory _fileInfoWrapperFactory;
        private readonly IExcelFileBuilder _excelFileBuilder;

        public TestResultsSpreadsheetBuilder(IFileLocationProvider locationProvider,
            IDirectoryInfoWrapperFactory directoryInfoWrapperFactory,
            IFileInfoWrapperFactory fileInfoWrapperFactory,
            IExcelFileBuilder excelFileBuilder)
        {
            _spreadsheetDirectory = locationProvider.GetSpreadsheetDirectoryPath();
            _directoryInfoWrapperFactory = directoryInfoWrapperFactory;
            _fileInfoWrapperFactory = fileInfoWrapperFactory;
            _excelFileBuilder = excelFileBuilder;
        }

        public async Task CreateTestResultsSpreadsheetAsync(Class @class, Test test, Application.Models.Spreadsheet.Metadata metadata)
        {
            IDirectoryInfoWrapper directory = _directoryInfoWrapperFactory.Create(_spreadsheetDirectory);
            string newFilePath = System.IO.Path.Combine(directory.FullName, $"{metadata.FileName}.xlsx");
            IFileInfoWrapper newFile = _fileInfoWrapperFactory.Create(Path.CreateFromString(newFilePath));
            var fileBuilder = _excelFileBuilder.CreateNew(newFile.GetFileInfo());

            var spreadsheetWriter = fileBuilder.CreateSpreadsheetWriter(metadata.FileName);
            int questionNamesColumns = test.GetMaximumQuestionNamesPerAssignment();

            var titleInput = new TitleBlock.TitleBlockInput(spreadsheetWriter.CurrentPosition,
                test.Name,
                metadata.Created);
            var titleBlock = new TitleBlock(titleInput);
            titleBlock
                .Write(spreadsheetWriter);

            var configurationInput = new ConfigurationBlock.ConfigurationBlockInput(spreadsheetWriter.CurrentPosition,
                test.GetMaximumPoints(),
                test.StandardizationFactor,
                test.MinimumGrade);
            var configurationBlock = new ConfigurationBlock(configurationInput);
            configurationBlock.Write(spreadsheetWriter);

            spreadsheetWriter
                .NewLine()
                .NewLine()
                .MoveRightTimes(questionNamesColumns);
            var studentNamesInput = new StudentNamesBlock.StudentNamesBlockInput(spreadsheetWriter.CurrentPosition, @class.Students);
            var studentNamesBlock = new StudentNamesBlock(studentNamesInput);
            studentNamesBlock.Write(spreadsheetWriter);
            var studentNamesRowStartpoint = new Point(spreadsheetWriter.CurrentPosition.X + 1, spreadsheetWriter.CurrentPosition.Y);

            spreadsheetWriter
                .NewLine()
                .Write("Opgave")
                .MoveRightTimes(questionNamesColumns)
                .Write("Punten")
                .NewLine();
            var questionNamesColumnTopLeft = spreadsheetWriter.CurrentPosition;

            bool switchColor = false;
            foreach (Assignment assignment in test.Assignments)
            {
                var color = switchColor ? _tableAssignmentRowColor : Color.White;
                spreadsheetWriter.SetBackgroundColor(color);
                foreach (Question question in assignment.Questions)
                {
                    foreach (Name questionName in question.QuestionNames)
                    {
                        spreadsheetWriter
                            .Write(questionName.Value)
                            .MoveRight();
                    }
                    spreadsheetWriter
                        .Write(question.MaximumScore.Value)
                        .NewLine();
                }
                switchColor = !switchColor;
            }
            spreadsheetWriter.ResetStyling();
            spreadsheetWriter.Write("Totaal:")
                .MoveRight();
            var maximumPointsColumnTop = new Point(questionNamesColumnTopLeft.X + questionNamesColumns, questionNamesColumnTopLeft.Y);
            var maximumPointsColumnBottom = new Point(spreadsheetWriter.CurrentPosition.X, spreadsheetWriter.CurrentPosition.Y - 1);
            spreadsheetWriter.PlaceStandardFormula(maximumPointsColumnTop, maximumPointsColumnBottom, FormulaType.SUM);
            // var gradeFormula = BuildGradeFormula(spreadsheetWriter.CurrentCell,
            //configurationBlock.MaximumPointsPosition, 
            //    configurationBlock.StandardizationfactorPosition, 
            //     configurationBlock.MinimumGradePosition);
            spreadsheetWriter
                .NewLine()
                .Write("Cijfer")
                .MoveRight();
            //     .PlaceCustomFormula(gradeFormula);

            var gradeRowStart = spreadsheetWriter.CurrentPosition;

            spreadsheetWriter.CurrentPosition = studentNamesRowStartpoint;
            foreach (Student student in @class.Students)
            {
                Point studentColumnTop = spreadsheetWriter.CurrentPosition;
                spreadsheetWriter
                    .SetTextRotation(40)
                    .Write(student.FirstName.Value)
                    .Write(student.Infix) // TODO: spacing
                    .Write(student.LastName.Value)
                    .ResetStyling();

                var scoredPointsColumnStart = new Point(spreadsheetWriter.CurrentPosition.X, maximumPointsColumnTop.Y);
                var scoredPointsColumnEnd = new Point(spreadsheetWriter.CurrentPosition.X, maximumPointsColumnBottom.Y);

                spreadsheetWriter.CurrentPosition = scoredPointsColumnEnd;
                spreadsheetWriter.MoveDown()
                    .PlaceStandardFormula(scoredPointsColumnStart, scoredPointsColumnEnd, FormulaType.SUM);
                //    IFormulaBuilder studentGradeFormula = BuildGradeFormula(spreadsheetWriter.CurrentCell, maximumPointsCell, standardizationfactorCell, miniumGradeCell);
                //spreadsheetWriter.MoveDown()
                //.PlaceCustomFormula(studentGradeFormula);

                spreadsheetWriter.CurrentPosition = studentColumnTop;
                spreadsheetWriter.MoveRight();
            }
            var studentNamesRowEndPoint = new Point(spreadsheetWriter.CurrentPosition.X - 1, spreadsheetWriter.CurrentPosition.Y);

            var pointsRowAverageStart = new Point(maximumPointsColumnBottom.X + 1, maximumPointsColumnBottom.Y + 1);
            var pointsRowAverageEnd = new Point(studentNamesRowEndPoint.X, maximumPointsColumnBottom.Y + 1);
            spreadsheetWriter.CurrentPosition = gradeRowStart;
            spreadsheetWriter
                .NewLine()
                .NewLine()
                .Write("Gemiddeld aantal punten")
                .MoveRight()
                .PlaceStandardFormula(pointsRowAverageStart, pointsRowAverageEnd, FormulaType.AVERAGE)
                .NewLine();

            var gradeRowAverageStart = new Point(gradeRowStart.X + 1, gradeRowStart.Y);
            var gradeRowAverageEnd = new Point(studentNamesRowEndPoint.X, gradeRowStart.Y);
            spreadsheetWriter.Write("Gemiddeld cijfer")
                .MoveRight()
                .PlaceStandardFormula(gradeRowAverageStart, gradeRowAverageEnd, FormulaType.AVERAGE);

            fileBuilder.FillMetadata(metadata.MapToLibraryModel());

            await fileBuilder.SaveAsync();
        }

        private IFormulaBuilder BuildGradeFormula(ExcelRange achievedPoints,
            ExcelRange maximumPoints,
            ExcelRange standardizationFactor,
            ExcelRange minimumGrade)
        {
            var builder = new FormulaBuilder();
            builder.AddEqualsSign()
                .AddOpenParenthesis()
                .AddCellAddress(achievedPoints.Address)
                .AddDivideSign()
                .AddCellAddress(maximumPoints.Address)
                .AddClosedParenthesis()
                .AddMultiplySign()
                .AddCellAddress(standardizationFactor.Address)
                .AddSumSign()
                .AddCellAddress(minimumGrade.Address);
            return builder;
        }
    }
}
