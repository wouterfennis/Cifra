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
        private const BorderStyle AssignmentBorder = BorderStyle.Medium;
        public Point StartPoint { get; }
        public IEnumerable<int> AssignmentBottomRows { get; }
        public int MostRightColumn { get; }

        public BorderBlock(Point startPoint, IEnumerable<int> assignmentBottomRows, int mostRightColumn)
        {
            StartPoint = startPoint;
            AssignmentBottomRows = assignmentBottomRows;
            MostRightColumn = mostRightColumn;
        }

        public void Write(ISpreadsheetWriter spreadsheetWriter)
        {
            for (int assignmentIndex = 0; assignmentIndex < AssignmentBottomRows.Count(); assignmentIndex++)
            {
                var assignmentBottomRow = AssignmentBottomRows.ElementAt(assignmentIndex);
                spreadsheetWriter.CurrentPosition = new Point(0, assignmentBottomRow);

                for (int columIndex = 0; columIndex < MostRightColumn; columIndex++)
                {
                    var cell = spreadsheetWriter.GetCellRange(spreadsheetWriter.CurrentPosition);
                    spreadsheetWriter.Write(cell.Value);
                }
            }
        }
    }
}
