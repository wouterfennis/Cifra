using OfficeOpenXml;
using SpreadsheetWriter.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadsheetWriter.EPPlus
{
    public class ExcelRangeWrapper : IExcelRange
    {
        ExcelRange _excelRange;

        public ExcelRangeWrapper(ExcelRange excelAddress)
        {
            _excelRange = excelAddress;
        }

        public string Address => _excelRange.Address;

        public object Value { get => _excelRange.Value; set => _excelRange.Value = value; }

        public string Formula { get => _excelRange.Formula; set => _excelRange.Formula = value; }
    }
}
