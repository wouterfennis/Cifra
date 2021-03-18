using SpreadsheetWriter.Abstractions.Cell;

namespace SpreadsheetWriter.Test
{
    /// <inheritdoc/>
    public class TestExcelAddress : ICellAddress
    {
        private string _columnLetter;
        private string _rowNumber;

        public TestExcelAddress()
        {
            _columnLetter = "columnLetter";
            _rowNumber = "rowNumber";
        }

        public ColumnLetter ColumnLetter => ColumnLetter.Create(_columnLetter);

        public RowNumber RowNumber => RowNumber.Create(_rowNumber);
    }
}
