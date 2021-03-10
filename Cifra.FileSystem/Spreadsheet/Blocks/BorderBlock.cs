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
            DrawHeaderBorder(spreadsheetWriter);
            DrawAssignmentBorders(spreadsheetWriter);
            DrawTotalRowBorder(spreadsheetWriter);
            DrawGradeRowBorder(spreadsheetWriter);

            spreadsheetWriter.ResetStyling();
        }

        private void DrawAssignmentBorders(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.SetBorder(BorderStyle.Thin, BorderDirection.Bottom);
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

        private void DrawHeaderBorder(ISpreadsheetWriter spreadsheetWriter)
        {
            // Styling is lost of previous data
            spreadsheetWriter.SetBorder(BorderStyle.Thin, BorderDirection.Bottom);
            for (int columnIndex = 1; columnIndex <= MostRightColumn; columnIndex++)
            {
                DrawBorder(spreadsheetWriter, columnIndex, HeaderRow);
            }
            spreadsheetWriter.ResetStyling();
        }

        private void DrawTotalRowBorder(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.SetBorder(BorderStyle.Double, BorderDirection.Bottom);
            for (int columnIndex = 1; columnIndex <= MostRightColumn; columnIndex++)
            {
                DrawBorder(spreadsheetWriter, columnIndex, TotalRow);
            }
            spreadsheetWriter.ResetStyling();
        }

        private void DrawGradeRowBorder(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.SetBorder(BorderStyle.Double, BorderDirection.Bottom);
            for (int columnIndex = 1; columnIndex <= MostRightColumn; columnIndex++)
            {
                DrawBorder(spreadsheetWriter, columnIndex, GradeRow);
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
