using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Formula;
using System.Drawing;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to write grades.
    /// </summary>
    internal class StatisticsBlock
    {
        public Point StartPosition { get; }

        public int AchievedScoresRow { get; }

        public int GradesRow { get; }

        public int StartOfStudentsColumn { get; }

        public int NumberOfStudents { get; }

        public StatisticsBlock(
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

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.CurrentPosition = StartPosition;

            var scoresStartPosition = new Point(StartOfStudentsColumn, AchievedScoresRow);
            var scoresEndPosition = new Point(StartOfStudentsColumn + NumberOfStudents, AchievedScoresRow);

            spreadsheetWriter
                .SetFontBold(true)
                .Write("Gemiddelde aantal punten")
                .SetFontBold(false)
                .MoveRight()
                .PlaceStandardFormula(scoresStartPosition, scoresEndPosition, FormulaType.AVERAGE)
                .NewLine();

            var gradesStartPosition = new Point(StartOfStudentsColumn, GradesRow);
            var gradesEndPosition = new Point(StartOfStudentsColumn + NumberOfStudents, GradesRow);
            spreadsheetWriter
                .SetFontBold(true)
                .Write("Gemiddelde cijfer")
                .SetFontBold(false)
                .MoveRight()
                .PlaceStandardFormula(gradesStartPosition, gradesEndPosition, FormulaType.AVERAGE);
        }
    }
}
