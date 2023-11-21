using SpreadsheetWriter.Abstractions.Cell;

namespace SpreadsheetWriter.Test
{
    /// <inheritdoc/>
    public class TestExcelAddress : ICellAddress
    {
        private readonly string _columnLetter;
        private readonly string _rowNumber;

        public TestExcelAddress()
        {
            _columnLetter = "columnLetter";
            _rowNumber = "rowNumber";
        }

        public ColumnLetter ColumnLetter => ColumnLetter.Create(_columnLetter);

        public RowNumber RowNumber => RowNumber.Create(_rowNumber);

        public override string ToString()
        {
            return $"{_columnLetter}{_rowNumber}";
        }
    }
}
