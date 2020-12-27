using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Mapping;
using Cifra.FileSystem.Spreadsheet.Blocks;
using OfficeOpenXml;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.EPPlus;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Cifra.FileSystem.Spreadsheet
{
    public class TestResultsSpreadsheetBuilder : ITestResultsSpreadsheetFactory
    {
        private readonly Color _tableAssignmentRowColor = Color.FromArgb(217, 225, 242);
        private readonly IFileLocationProvider _locationProvider;
        private readonly ISpreadsheetFileFactory _spreadsheetFileFactory;

        public TestResultsSpreadsheetBuilder(IFileLocationProvider locationProvider,
            ISpreadsheetFileFactory spreadsheetFileFactory)
        {
            _locationProvider = locationProvider;
            _spreadsheetFileFactory = spreadsheetFileFactory;
        }

        public async Task CreateTestResultsSpreadsheetAsync(Class @class, Test test, Application.Models.Spreadsheet.Metadata metadata)
        {
            var libraryMetadata = metadata.MapToLibraryModel();
            ISpreadsheetFile spreadsheetFile = _spreadsheetFileFactory.Create(_locationProvider.GetSpreadsheetDirectoryPath().Value, libraryMetadata);

            ISpreadsheetWriter spreadsheetWriter = spreadsheetFile.GetSpreadsheetWriter();
            int questionNamesColumns = test.GetMaximumQuestionNamesPerAssignment();

            AddTitle(test, metadata, spreadsheetWriter);

            AddConfiguration(test, spreadsheetWriter);

            AddHeaders(@class, test, spreadsheetWriter, questionNamesColumns);

            AddTotalPointsRow();

            AddGradeRow();

            AddAverageResults();

            //var studentNamesRowStartpoint = new Point(spreadsheetWriter.CurrentPosition.X + 1, spreadsheetWriter.CurrentPosition.Y);
            //var questionNamesColumnTopLeft = spreadsheetWriter.CurrentPosition;

            //spreadsheetWriter.ResetStyling();
            //spreadsheetWriter.Write("Totaal:")
            //    .MoveRight();
            //var maximumPointsColumnTop = new Point(questionNamesColumnTopLeft.X + questionNamesColumns, questionNamesColumnTopLeft.Y);
            //var maximumPointsColumnBottom = new Point(spreadsheetWriter.CurrentPosition.X, spreadsheetWriter.CurrentPosition.Y - 1);
            ////            spreadsheetWriter.PlaceStandardFormula(maximumPointsColumnTop, maximumPointsColumnBottom, FormulaType.SUM);
            //// var gradeFormula = BuildGradeFormula(spreadsheetWriter.CurrentCell,
            ////configurationBlock.MaximumPointsPosition, 
            ////    configurationBlock.StandardizationfactorPosition, 
            ////     configurationBlock.MinimumGradePosition);
            //spreadsheetWriter
            //    .NewLine()
            //    .Write("Cijfer")
            //    .MoveRight();
            ////     .PlaceCustomFormula(gradeFormula);

            //var gradeRowStart = spreadsheetWriter.CurrentPosition;

            //spreadsheetWriter.CurrentPosition = studentNamesRowStartpoint;
            //foreach (Student student in @class.Students)
            //{
            //    Point studentColumnTop = spreadsheetWriter.CurrentPosition;
            //    spreadsheetWriter
            //        .SetTextRotation(40)
            //        .Write(student.FirstName.Value)
            //        .Write(student.Infix) // TODO: spacing
            //        .Write(student.LastName.Value)
            //        .ResetStyling();

            //    var scoredPointsColumnStart = new Point(spreadsheetWriter.CurrentPosition.X, maximumPointsColumnTop.Y);
            //    var scoredPointsColumnEnd = new Point(spreadsheetWriter.CurrentPosition.X, maximumPointsColumnBottom.Y);

            //    spreadsheetWriter.CurrentPosition = scoredPointsColumnEnd;
            //    //  spreadsheetWriter.MoveDown()
            //    //      .PlaceStandardFormula(scoredPointsColumnStart, scoredPointsColumnEnd, FormulaType.SUM);
            //    //    IFormulaBuilder studentGradeFormula = BuildGradeFormula(spreadsheetWriter.CurrentCell, maximumPointsCell, standardizationfactorCell, miniumGradeCell);
            //    //spreadsheetWriter.MoveDown()
            //    //.PlaceCustomFormula(studentGradeFormula);

            //    spreadsheetWriter.CurrentPosition = studentColumnTop;
            //    spreadsheetWriter.MoveRight();
            //}
            //var studentNamesRowEndPoint = new Point(spreadsheetWriter.CurrentPosition.X - 1, spreadsheetWriter.CurrentPosition.Y);

            //var pointsRowAverageStart = new Point(maximumPointsColumnBottom.X + 1, maximumPointsColumnBottom.Y + 1);
            //var pointsRowAverageEnd = new Point(studentNamesRowEndPoint.X, maximumPointsColumnBottom.Y + 1);
            //spreadsheetWriter.CurrentPosition = gradeRowStart;
            //spreadsheetWriter
            //    .NewLine()
            //    .NewLine()
            //    .Write("Gemiddeld aantal punten")
            //    .MoveRight()
            //    .PlaceStandardFormula(pointsRowAverageStart, pointsRowAverageEnd, FormulaType.AVERAGE)
            //    .NewLine();

            //var gradeRowAverageStart = new Point(gradeRowStart.X + 1, gradeRowStart.Y);
            //var gradeRowAverageEnd = new Point(studentNamesRowEndPoint.X, gradeRowStart.Y);
            //spreadsheetWriter.Write("Gemiddeld cijfer")
            //    .MoveRight()
            //    .PlaceStandardFormula(gradeRowAverageStart, gradeRowAverageEnd, FormulaType.AVERAGE);

            await spreadsheetFile.SaveAsync();
        }

        private void AddTotalPointsRow()
        {
            throw new NotImplementedException();
        }

        private void AddGradeRow()
        {
            throw new NotImplementedException();
        }

        private void AddAverageResults()
        {
            throw new NotImplementedException();
        }

        private static void AddHeaders(Class @class, Test test, ISpreadsheetWriter spreadsheetWriter, int questionNamesColumns)
        {
            spreadsheetWriter
                .NewLine()
                .NewLine();
            var assignmentsBlockInput = new AssignmentsBlock.AssignmentsBlockInput(spreadsheetWriter.CurrentPosition,
                test.Assignments);
            var assignmentsBlock = new AssignmentsBlock(assignmentsBlockInput);
            assignmentsBlock.Write(spreadsheetWriter);

            spreadsheetWriter.CurrentPosition = assignmentsBlock.PointsHeaderPosition;
            spreadsheetWriter.MoveRight();
            var studentNamesInput = new StudentNamesBlock.StudentNamesBlockInput(spreadsheetWriter.CurrentPosition, @class.Students);
            var studentNamesBlock = new StudentNamesBlock(studentNamesInput);
            studentNamesBlock.Write(spreadsheetWriter);
        }

        private static void AddConfiguration(Test test, ISpreadsheetWriter spreadsheetWriter)
        {
            var configurationInput = new ConfigurationBlock.ConfigurationBlockInput(spreadsheetWriter.CurrentPosition,
                test.GetMaximumPoints(),
                test.StandardizationFactor,
                test.MinimumGrade);
            var configurationBlock = new ConfigurationBlock(configurationInput);
            spreadsheetWriter
                .NewLine()
                .NewLine();
            configurationBlock.Write(spreadsheetWriter);
        }

        private static void AddTitle(Test test, Application.Models.Spreadsheet.Metadata metadata, ISpreadsheetWriter spreadsheetWriter)
        {
            var titleInput = new TitleBlock.TitleBlockInput(spreadsheetWriter.CurrentPosition,
                test.Name,
                metadata.Created);
            var titleBlock = new TitleBlock(titleInput);
            titleBlock
                .Write(spreadsheetWriter);
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
