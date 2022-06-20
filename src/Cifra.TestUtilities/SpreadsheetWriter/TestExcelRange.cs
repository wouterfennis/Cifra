using SpreadsheetWriter.Abstractions.Cell;
using System.Diagnostics.CodeAnalysis;

namespace SpreadsheetWriter.Test
{
    /// <inheritdoc/>
    [ExcludeFromCodeCoverage] // Part of test project.
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
