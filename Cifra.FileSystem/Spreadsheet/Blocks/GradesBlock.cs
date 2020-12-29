using SpreadsheetWriter.Abstractions;
using System.Drawing;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write grades.
    /// </summary>
    internal class GradesBlock
    {
        private readonly GradesBlockInput input;

        public GradesBlock(GradesBlockInput input)
        {
            this.input = input;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = input.StartPosition;
            spreadsheetWriter
                .Write("Cijfer")
                .MoveRightTimes(input.ScoreTopPoint.X);
            const int maximumPointsColumn = 1;
            int numberOfScoreColumns = input.NumberOfStudents + maximumPointsColumn;
            for (int i = 0; i < numberOfScoreColumns; i++)
            {
                var startPosition = new Point(spreadsheetWriter.CurrentPosition.X, input.ScoreTopPoint.Y);
                var endPosition = new Point(spreadsheetWriter.CurrentPosition.X, spreadsheetWriter.CurrentPosition.Y);
                spreadsheetWriter
                    .PlaceCustomFormula(SetupGradeFormula())
                    .MoveRight();
            }
        }

        private IFormulaBuilder SetupGradeFormula(IExcelRange achievedPoints,
            IExcelRange maximumPoints,
            IExcelRange standardizationFactor,
            IExcelRange minimumGrade)
        {
            return input.FormulaBuilder
                .AddEqualsSign()
                .AddOpenParenthesis()
                .AddCellAddress(achievedPoints.Address)
                .AddDivideSign()
                .AddCellAddress(maximumPoints.Address)
                .AddClosedParenthesis()
                .AddMultiplySign()
                .AddCellAddress(standardizationFactor.Address)
                .AddSumSign()
                .AddCellAddress(minimumGrade.Address);
        }

        public class GradesBlockInput
        {
            public Point StartPosition { get; }

            public IFormulaBuilder FormulaBuilder { get; }

            public Point AchievedScorePosition { get; }

            public Point MaximumScorePosition { get; }

            public Point StandardizationFactorPosition { get; }

            public int NumberOfStudents { get; }

            public GradesBlockInput(
                Point startPosition,
                IFormulaBuilder formulaBuilder,
                Point achievedScorePosition, 
                Point maximumScorePosition, 
                Point standardizationFactorPosition,
                int numberOfStudents)
            {
                StartPosition = startPosition;
                FormulaBuilder = formulaBuilder;
                AchievedScorePosition = achievedScorePosition;
                MaximumScorePosition = maximumScorePosition;
                StandardizationFactorPosition = standardizationFactorPosition;
                NumberOfStudents = numberOfStudents;
            }
        }
    }
}
