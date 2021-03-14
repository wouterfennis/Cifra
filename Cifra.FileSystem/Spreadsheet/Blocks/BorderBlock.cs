using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Styling;

namespace Cifra.FileSystem.Spreadsheet.Blocks
{
    /// <summary>
    /// Spreadsheet block to apply borders.
    /// </summary>
    internal class BorderBlock
    {
        public int HeaderRow { get; }

        public IEnumerable<int> AssignmentBottomRows { get; }

        public int TotalRow { get; }

        public int GradeRow { get; }

        public int MostRightColumn { get; }

        public BorderBlock(int headerRow,
            IEnumerable<int> assignmentBottomRows,
            int totalRow,
            int gradeRow,
            int mostRightColumn)
        {
            HeaderRow = headerRow;
            AssignmentBottomRows = assignmentBottomRows;
            TotalRow = totalRow;
            GradeRow = gradeRow;
            MostRightColumn = mostRightColumn;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            DrawAssignmentBorders(spreadsheetWriter);

            spreadsheetWriter.ResetStyling();
        }

        private void DrawAssignmentBorders(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.SetBorder(BorderStyle.Thin, BorderDirection.Bottom, Color.Black);
            for (int assignmentIndex = 0; assignmentIndex < AssignmentBottomRows.Count(); assignmentIndex++)
            {
                var assignmentBottomRow = AssignmentBottomRows.ElementAt(assignmentIndex);

                for (int columnIndex = 1; columnIndex <= MostRightColumn; columnIndex++)
                {
                    DrawBorder(spreadsheetWriter, columnIndex, assignmentBottomRow);
                }
            }
            spreadsheetWriter.ResetStyling();
        }

        private static void DrawBorder(ISpreadsheetWriter spreadsheetWriter, int x, int y)
        {
            spreadsheetWriter.CurrentPosition = new Point(x, y);
            var cell = spreadsheetWriter.GetCellRange(spreadsheetWriter.CurrentPosition);
            spreadsheetWriter.Write(cell.Value);
        }
    }
}
