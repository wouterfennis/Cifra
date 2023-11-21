using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Cell;
using SpreadsheetWriter.Abstractions.Formula;
using SpreadsheetWriter.EPPlus.Formula;
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
                .PlaceStandardFormula(gradesStartPosition, gradesEndPosition, FormulaType.AVERAGE)
                .NewLine();

            ICellRange gradeStartCell = spreadsheetWriter.GetCellRange(gradesStartPosition);
            ICellRange gradeEndCell = spreadsheetWriter.GetCellRange(gradesEndPosition);

            var numberOfBadGradesFormula = new FormulaBuilder()
                .AddEqualsSign()
                .AddFormulaType(FormulaType.COUNTIF)
                .AddOpenParenthesis()
                .AddCellAddress(gradeStartCell.Address)
                .AddColon()
                .AddCellAddress(gradeEndCell.Address)
                .AddComma()
                .AddCriteria("<5.5")
                .AddClosingParenthesis();

            spreadsheetWriter
                .SetFontBold(true)
                .Write("Aantal onvoldoendes")
                .SetFontBold(false)
                .MoveRight()
                .PlaceCustomFormula(numberOfBadGradesFormula)
                .NewLine();

            var percentageOfTotalBadGradesFormula = new FormulaBuilder()
                .AddEqualsSign()
                .AddFormulaType(FormulaType.COUNTIF)
                .AddOpenParenthesis()
                .AddCellAddress(gradeStartCell.Address)
                .AddColon()
                .AddCellAddress(gradeEndCell.Address)
                .AddComma()
                .AddCriteria("<5.5")
                .AddClosingParenthesis()
                .AddMultiplicationSign()
                .AddValue(100.0)
                .AddDivisionSign()
                .AddFormulaType(FormulaType.COUNT)
                .AddOpenParenthesis()
                .AddCellAddress(gradeStartCell.Address)
                .AddColon()
                .AddCellAddress(gradeEndCell.Address)
                .AddClosingParenthesis();

            spreadsheetWriter
                .SetFontBold(true)
                .Write("Percentage onvoldoendes van totaal")
                .SetFontBold(false)
                .MoveRight()
                .PlaceCustomFormula(percentageOfTotalBadGradesFormula);
        }
    }
}
