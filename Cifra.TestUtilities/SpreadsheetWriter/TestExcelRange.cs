using SpreadsheetWriter.Abstractions;

namespace SpreadsheetWriter.Test
{
    /// <inheritdoc/>
    public class TestExcelRange : ICellRange
    {
        private readonly string _address;
        private readonly string _value;

        public TestExcelRange(string address, string value)
        {
            _address = address;
            _value = value;
        }

        /// <inheritdoc/>
        public string Address => _address;

        /// <inheritdoc/>
        public string Value => _value;
    }
}
