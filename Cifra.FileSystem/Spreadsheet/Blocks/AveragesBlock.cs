using SpreadsheetWriter.Abstractions;
using System.Drawing;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write grades.
    /// </summary>
    internal class AveragesBlock
    {
        private readonly AveragesBlockInput input;

        public AveragesBlock(AveragesBlockInput input)
        {
            this.input = input;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = input.StartPosition;

            var scoresStartPosition = new Point(input.ScoresStartColumn, input.AchievedScoresRow);
            var scoresEndPosition = new Point(input.ScoresStartColumn + input.NumberOfStudents, input.AchievedScoresRow);
            spreadsheetWriter
                .Write("Gemiddeld aantal punten")
                .MoveRight()
                .PlaceStandardFormula(scoresStartPosition, scoresEndPosition, FormulaType.AVERAGE);
        }

        public class AveragesBlockInput
        {
            public Point StartPosition { get; }

            public int AchievedScoresRow { get; }

            public int GradesRow { get; }

            public int ScoresStartColumn { get; }

            public int NumberOfStudents { get; }

            public AveragesBlockInput(
                Point startPosition,
                int achievedScoreRow,
                int gradesRow,
                int scoresStartColumn,
                int numberOfStudents
                )
            {
                StartPosition = startPosition;
                AchievedScoresRow = achievedScoreRow;
                GradesRow = gradesRow;
                ScoresStartColumn = scoresStartColumn;
                NumberOfStudents = numberOfStudents;
            }
        }
    }
}
