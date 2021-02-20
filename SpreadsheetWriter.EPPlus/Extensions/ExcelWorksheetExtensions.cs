using System;
using System.Drawing;
using OfficeOpenXml;

namespace SpreadsheetWriter.EPPlus.Extensions
{
    /// <summary>
    /// Extensions for the <see cref="ExcelWorksheet"/>.
    /// </summary>
    internal static class ExcelWorksheetExtensions
    {
        /// <summary>
        /// Get a specific cell from a <see cref="ExcelWorksheet"/>.
        /// </summary>
        /// <param name="excelWorksheet">The worksheet to pick the cell.</param>
        /// <param name="point">The point where the cell is.</param>
        public static ExcelRange GetCell(this ExcelWorksheet excelWorksheet, Point point)
        {
            if (excelWorksheet?.Cells == null)
            {
                throw new ArgumentException(ExceptionMessages.ExcelWorksheetNull);
            }
            return excelWorksheet.Cells[point.Y, point.X];
        }
    }
}
