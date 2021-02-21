using System;
using OfficeOpenXml;
using SpreadsheetWriter.Abstractions;

namespace SpreadsheetWriter.EPPlus
{
    /// <inheritdoc/>
    public class ExcelRangeWrapper : ICellRange
    {
        ExcelRange _excelRange;

        public ExcelRangeWrapper(ExcelRange excelRange)
        {
            _excelRange = excelRange ?? throw new ArgumentNullException(nameof(excelRange));
        }

        /// <inheritdoc/>
        public string Address => _excelRange.Address;
    }
}
