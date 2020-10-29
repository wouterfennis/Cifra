using OfficeOpenXml;
using System.Drawing;

namespace WorksheetWriter.EPPlus.Extensions
{
    public static class ExcelWorksheetExtensions
    {
        public static ExcelRange GetCell(this ExcelWorksheet excelWorksheet, Point point)
        {
            return excelWorksheet.Cells[point.Y, point.X];
        }
    }
}
