using System.Drawing;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Formula;
using SpreadsheetWriter.Abstractions.Styling;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write total scores.
    /// </summary>
    internal class TotalScoresBlock
    {
        public Point StartPoint { get; }

        public Point ScoreTopPoint { get; }

        public int NumberOfStudents { get; }

        public int NumberOfVerions { get; }

        public TotalScoresBlock(
            Point startPoint,
            Point scoreTopPoint,
            int numberOfStudents)
        {
            StartPoint = startPoint;
            ScoreTopPoint = scoreTopPoint;
            NumberOfStudents = numberOfStudents;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = StartPoint;
            spreadsheetWriter
                .SetFontBold(true)
                .SetBorder(BorderStyle.Double, BorderDirection.Bottom, Color.Black)
                .Write("Totaal")
                .SetFontBold(false);

            int columnsBetweenFirstPoint = ScoreTopPoint.X - spreadsheetWriter.CurrentPosition.X;
            for (int i = 0; i < columnsBetweenFirstPoint; i++)
            {
                spreadsheetWriter.MoveRight();
                spreadsheetWriter.Write(string.Empty);
            }

            const int maximumPointsColumn = 1;
            int numberOfScoreColumns = NumberOfStudents + maximumPointsColumn;
            for (int columnIndex = 0; columnIndex < numberOfScoreColumns; columnIndex++)
            {
                var startPosition = new Point(spreadsheetWriter.CurrentPosition.X, ScoreTopPoint.Y);
                var lastQuestionRowOffset = 1;
                var endPosition = new Point(spreadsheetWriter.CurrentPosition.X, spreadsheetWriter.CurrentPosition.Y - lastQuestionRowOffset);
                spreadsheetWriter
                    .PlaceStandardFormula(startPosition, endPosition, FormulaType.SUM)
                    .MoveRight();
            }
        }
    }
}
