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
        private const BorderStyle AssignmentBorderStyle = BorderStyle.Medium;
        private const BorderDirection AssignmentBorderDirection = BorderDirection.Bottom;
        public IEnumerable<int> AssignmentBottomRows { get; }
        public int MostRightColumn { get; }

        public BorderBlock(IEnumerable<int> assignmentBottomRows, int mostRightColumn)
        {
            AssignmentBottomRows = assignmentBottomRows;
            MostRightColumn = mostRightColumn;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            spreadsheetWriter.SetBorder(AssignmentBorderStyle, AssignmentBorderDirection);
            for (int assignmentIndex = 0; assignmentIndex < AssignmentBottomRows.Count(); assignmentIndex++)
            {
                var assignmentBottomRow = AssignmentBottomRows.ElementAt(assignmentIndex);
                spreadsheetWriter.CurrentPosition = new Point(0, assignmentBottomRow);

                for (int columnIndex = 0; columnIndex < MostRightColumn; columnIndex++)
                {
                    spreadsheetWriter.CurrentPosition = new Point(columnIndex, assignmentBottomRow);
                    var cell = spreadsheetWriter.GetCellRange(spreadsheetWriter.CurrentPosition);
                    spreadsheetWriter.Write(cell.Value);
                }
            }
            spreadsheetWriter.ResetStyling();
        }
    }
}
