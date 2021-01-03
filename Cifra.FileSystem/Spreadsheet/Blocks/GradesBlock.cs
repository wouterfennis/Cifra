using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Formula;
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
                .Write("Cijfer");

            spreadsheetWriter.CurrentPosition = new Point(input.ScoresStartColumn, spreadsheetWriter.CurrentPosition.Y);

            IExcelRange maximumScoreCell = spreadsheetWriter.GetExcelRange(input.MaximumScorePosition);
            IExcelRange standardizationFactorCell = spreadsheetWriter.GetExcelRange(input.StandardizationFactorPosition);
            IExcelRange minimumGradeCell = spreadsheetWriter.GetExcelRange(input.MinimumGradePosition);

            const int maximumPointsColumn = 1;
            int numberOfScoreColumns = input.NumberOfStudents + maximumPointsColumn;
            for (int columnIndex = 0; columnIndex < numberOfScoreColumns; columnIndex++)
            {
                var achievedScorePosition = new Point(spreadsheetWriter.CurrentPosition.X, input.AchievedScoresRow);
                IExcelRange achievedScoreCell = spreadsheetWriter.GetExcelRange(achievedScorePosition);

                spreadsheetWriter
                    .PlaceCustomFormula(SetupGradeFormula(achievedScoreCell,
                    maximumScoreCell,
                    standardizationFactorCell,
                    minimumGradeCell))
                    .MoveRight();
            }
        }

        private IFormulaBuilder SetupGradeFormula(IExcelRange achievedPoints,
            IExcelRange maximumScore,
            IExcelRange standardizationFactor,
            IExcelRange minimumGrade)
        {
            IFormulaBuilder formulaBuilder = input.FormulaBuilderFactory.Create();
            return formulaBuilder
                .AddEqualsSign()
                .AddOpenParenthesis()
                .AddCellAddress(achievedPoints.Address)
                .AddDivideSign()
                .AddCellAddress(maximumScore.Address)
                .AddClosedParenthesis()
                .AddMultiplySign()
                .AddCellAddress(standardizationFactor.Address)
                .AddSumSign()
                .AddCellAddress(minimumGrade.Address);
        }

        public class GradesBlockInput
        {
            public Point StartPosition { get; }

            public IFormulaBuilderFactory FormulaBuilderFactory { get; }

            public int AchievedScoresRow { get; }

            public int ScoresStartColumn { get; }

            public Point MaximumScorePosition { get; }

            public Point StandardizationFactorPosition { get; }

            public Point MinimumGradePosition { get; }

            public int NumberOfStudents { get; }

            public GradesBlockInput(
                Point startPosition,
                IFormulaBuilderFactory formulaBuilderFactory,
                int achievedScoreRow,
                int scoresStartColumn,
                Point maximumScorePosition,
                Point standardizationFactorPosition,
                Point minimumScorePosition,
                int numberOfStudents)
            {
                StartPosition = startPosition;
                FormulaBuilderFactory = formulaBuilderFactory;
                AchievedScoresRow = achievedScoreRow;
                ScoresStartColumn = scoresStartColumn;
                MaximumScorePosition = maximumScorePosition;
                StandardizationFactorPosition = standardizationFactorPosition;
                MinimumGradePosition = minimumScorePosition;
                NumberOfStudents = numberOfStudents;
            }
        }
    }
}
