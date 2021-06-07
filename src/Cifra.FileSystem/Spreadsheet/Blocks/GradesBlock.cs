using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Cell;
using SpreadsheetWriter.Abstractions.Formula;
using SpreadsheetWriter.Abstractions.Styling;
using System.Drawing;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write grades.
    /// </summary>
    internal class GradesBlock
    {
        public Point StartPosition { get; }

        public IFormulaBuilderFactory FormulaBuilderFactory { get; }

        public int AchievedScoresRow { get; }

        public int ScoresStartColumn { get; }

        public Point MaximumScorePosition { get; }

        public Point StandardizationFactorPosition { get; }

        public Point MinimumGradePosition { get; }

        public int NumberOfStudents { get; }

        public GradesBlock(
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

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = StartPosition;
            spreadsheetWriter
                .SetFontBold(true)
                .SetBorder(BorderStyle.Double, BorderDirection.Bottom, Color.Black)
                .Write("Cijfer")
                .SetFontBold(false);

            int columnsBetweenFirstPoint = ScoresStartColumn - spreadsheetWriter.CurrentPosition.X;
            for (int i = 0; i < columnsBetweenFirstPoint; i++)
            {
                spreadsheetWriter.MoveRight();
                spreadsheetWriter.Write(string.Empty);
            }

            ICellRange maximumScoreCell = spreadsheetWriter.GetCellRange(MaximumScorePosition);
            ICellRange standardizationFactorCell = spreadsheetWriter.GetCellRange(StandardizationFactorPosition);
            ICellRange minimumGradeCell = spreadsheetWriter.GetCellRange(MinimumGradePosition);

            const int maximumPointsColumn = 1;
            int numberOfScoreColumns = NumberOfStudents + maximumPointsColumn;
            spreadsheetWriter.SetFormat("0.0");
            for (int columnIndex = 0; columnIndex < numberOfScoreColumns; columnIndex++)
            {
                var achievedScorePosition = new Point(spreadsheetWriter.CurrentPosition.X, AchievedScoresRow);
                ICellRange achievedScoreCell = spreadsheetWriter.GetCellRange(achievedScorePosition);

                spreadsheetWriter
                    .PlaceLessThanRule(5.5, Color.Red)
                    .PlaceCustomFormula(SetupGradeFormula(achievedScoreCell,
                    maximumScoreCell,
                    standardizationFactorCell,
                    minimumGradeCell))
                    .MoveRight();
            }
            spreadsheetWriter.ResetStyling();
        }

        private IFormulaBuilder SetupGradeFormula(ICellRange achievedPoints,
            ICellRange maximumScore,
            ICellRange standardizationFactor,
            ICellRange minimumGrade)
        {
            IFormulaBuilder formulaBuilder = FormulaBuilderFactory.Create();
            return formulaBuilder
                .AddEqualsSign()
                .AddOpenParenthesis()
                .AddCellAddress(achievedPoints.Address)
                .AddDivisionSign()
                .AddConstantSign()
                .AddCellColumnLetter(maximumScore.Address.ColumnLetter)
                .AddConstantSign()
                .AddRowNumber(maximumScore.Address.RowNumber)
                .AddClosingParenthesis()
                .AddMultiplicationSign()
                .AddConstantSign()
                .AddCellColumnLetter(standardizationFactor.Address.ColumnLetter)
                .AddConstantSign()
                .AddRowNumber(standardizationFactor.Address.RowNumber)
                .AddSummationSign()
                .AddConstantSign()
                .AddCellColumnLetter(minimumGrade.Address.ColumnLetter)
                .AddConstantSign()
                .AddRowNumber(minimumGrade.Address.RowNumber);

        }
    }
}
