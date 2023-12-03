using SpreadsheetWriter.Abstractions.View;

namespace SpreadsheetWriter.Test
{
    /// <inheritdoc/>
    public class TestExcelView : IView
    {
        public int RowNumber;
        public int ColumnNumber;

        public TestExcelView()
        {
            RowNumber = 0;
            ColumnNumber = 0;
        }

        public void FreezePanes(int row, int column)
        {
            RowNumber = row;
            ColumnNumber = column;
        }
    }
}
