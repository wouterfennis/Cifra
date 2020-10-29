using OfficeOpenXml;
using System.Drawing;

namespace SpreadsheetWriter.EPPlus.Extensions
{
    internal static class ExcelWorksheetExtensions
    {
        public static ExcelRange GetCell(this ExcelWorksheet excelWorksheet, Point point)
        {
            return excelWorksheet.Cells[point.Y, point.X];
        }
    }
}
