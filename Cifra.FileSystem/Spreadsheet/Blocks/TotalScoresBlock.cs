using System.Drawing;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Formula;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write total scores.
    /// </summary>
    internal class TotalScoresBlock
    {
        private readonly TotalScoresBlockInput input;

        public TotalScoresBlock(TotalScoresBlockInput input)
        {
            this.input = input;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = input.StartPoint;
            spreadsheetWriter
                .SetFontBold(true)
                .Write("Totaal")
                .SetFontBold(false);
            spreadsheetWriter.CurrentPosition = new Point(input.ScoreTopPoint.X, spreadsheetWriter.CurrentPosition.Y);

            const int maximumPointsColumn = 1;
            int numberOfScoreColumns = input.NumberOfStudents + maximumPointsColumn;
            for (int columnIndex = 0; columnIndex < numberOfScoreColumns; columnIndex++)
            {
                var startPosition = new Point(spreadsheetWriter.CurrentPosition.X, input.ScoreTopPoint.Y);
                var endPosition = new Point(spreadsheetWriter.CurrentPosition.X, spreadsheetWriter.CurrentPosition.Y - 1);
                spreadsheetWriter
                    .PlaceStandardFormula(startPosition, endPosition, FormulaType.SUM)
                    .MoveRight();
            }
        }

        public class TotalScoresBlockInput
        {
            public Point StartPoint { get; }

            public Point ScoreTopPoint { get; }

            public int NumberOfStudents { get; }

            public TotalScoresBlockInput(
                Point startPoint,
                Point scoreTopPoint,
                int numberOfStudents)
            {
                StartPoint = startPoint;
                ScoreTopPoint = scoreTopPoint;
                NumberOfStudents = numberOfStudents;
            }
        }
    }
}
