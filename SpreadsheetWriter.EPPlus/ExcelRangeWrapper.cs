using OfficeOpenXml;
using SpreadsheetWriter.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadsheetWriter.EPPlus
{
    public class ExcelRangeWrapper : ICellRange
    {
        ExcelRange _excelRange;

        public ExcelRangeWrapper(ExcelRange excelAddress)
        {
            _excelRange = excelAddress;
        }

        public string Address => _excelRange.Address;
    }
}
