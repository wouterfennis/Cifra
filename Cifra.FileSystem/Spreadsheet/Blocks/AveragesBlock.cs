using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Formula;
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

            var scoresStartPosition = new Point(input.StartOfStudentsColumn, input.AchievedScoresRow);
            var scoresEndPosition = new Point(input.StartOfStudentsColumn + input.NumberOfStudents, input.AchievedScoresRow);
            spreadsheetWriter
                .Write("Gemiddelde aantal punten")
                .MoveRight()
                .PlaceStandardFormula(scoresStartPosition, scoresEndPosition, FormulaType.AVERAGE)
                .NewLine();

            var gradesStartPosition = new Point(input.StartOfStudentsColumn, input.GradesRow);
            var gradesEndPosition = new Point(input.StartOfStudentsColumn + input.NumberOfStudents, input.GradesRow);
            spreadsheetWriter
                .Write("Gemiddelde cijfer")
                .MoveRight()
                .PlaceStandardFormula(gradesStartPosition, gradesEndPosition, FormulaType.AVERAGE);
        }

        public class AveragesBlockInput
        {
            public Point StartPosition { get; }

            public int AchievedScoresRow { get; }

            public int GradesRow { get; }

            public int StartOfStudentsColumn { get; }

            public int NumberOfStudents { get; }

            public AveragesBlockInput(
                Point startPosition,
                int achievedScoreRow,
                int gradesRow,
                int startOfStudents,
                int numberOfStudents
                )
            {
                StartPosition = startPosition;
                AchievedScoresRow = achievedScoreRow;
                GradesRow = gradesRow;
                StartOfStudentsColumn = startOfStudents;
                NumberOfStudents = numberOfStudents;
            }
        }
    }
}
