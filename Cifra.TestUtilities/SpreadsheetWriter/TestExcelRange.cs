using SpreadsheetWriter.Abstractions.Cell;

namespace SpreadsheetWriter.Test
{
    /// <inheritdoc/>
    public class TestExcelRange : ICellRange
    {
        private readonly ICellAddress _address;
        private readonly string _value;

        public TestExcelRange(ICellAddress address, string value)
        {
            _address = address;
            _value = value;
        }

        /// <inheritdoc/>
        public ICellAddress Address => _address;

        /// <inheritdoc/>
        public string Value => _value;
    }
}
