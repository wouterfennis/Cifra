using OfficeOpenXml;
using SpreadsheetWriter.Abstractions;

namespace SpreadsheetWriter.EPPlus
{
    /// <inheritdoc/>
    public class ExcelRangeWrapper : ICellRange
    {
        ExcelRange _excelRange;

        public ExcelRangeWrapper(ExcelRange excelAddress)
        {
            _excelRange = excelAddress;
        }

        /// <inheritdoc/>
        public string Address => _excelRange.Address;
    }
}
