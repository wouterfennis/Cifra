using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Styling;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

            for (int index = HeaderRow; index < TotalRow; index++)
            {
                var y = index + 1;

                for (int columnIndex = 1; columnIndex <= MostRightColumn; columnIndex++)
                {
                    spreadsheetWriter.CurrentPosition = new Point(columnIndex, y);
                    spreadsheetWriter.SetHorizontalAlignment(HorizontalAlignment.Center);

                    if (AssignmentBottomRows.Contains(y))
                    {
                        spreadsheetWriter.SetBorder(BorderStyle.Thin, BorderDirection.Bottom, Color.Black);
                    }
                    spreadsheetWriter.ApplyStyling();
                    spreadsheetWriter.ResetStyling();
                }
            }
        }
    }
}
