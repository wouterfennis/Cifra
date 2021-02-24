using System;
using System.Drawing;
using OfficeOpenXml;


namespace SpreadsheetWriter.EPPlus.Extensions
{
    /// <summary>
    ///  Extensions for the <see cref="ExcelRange"/>.
    /// </summary>
    internal static class ExcelRangeExtensions
    {
        /// <summary>
        /// Set the background color of an <see cref="ExcelRange"/>.
        /// </summary>
        public static void SetBackgroundColor(this ExcelRange excelRange, Color color)
        {
            _ = excelRange?.Style?.Fill?.BackgroundColor ?? throw new ArgumentException(ExceptionMessages.ExcelRangeNull);

            excelRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            excelRange.Style.Fill.BackgroundColor.SetColor(color);
        }

        /// <summary>
        /// Set the font color of an <see cref="ExcelRange"/>.
        /// </summary>
        public static void SetFontColor(this ExcelRange excelRange, Color color)
        {
            _ = excelRange?.Style?.Font?.Color ?? throw new ArgumentException(ExceptionMessages.ExcelRangeNull);

            excelRange.Style.Font.Color.SetColor(color);
        }

        /// <summary>
        /// Set the font size of an <see cref="ExcelRange"/>.
        /// </summary>
        public static void SetFontSize(this ExcelRange excelRange, float size)
        {
            _ = excelRange?.Style?.Font ?? throw new ArgumentException(ExceptionMessages.ExcelRangeNull);

            excelRange.Style.Font.Size = size;
        }

        /// <summary>
        /// Converts the cell type to Euro (€) of an <see cref="ExcelRange"/>.
        /// </summary>
        public static void ConvertToEuro(this ExcelRange excelRange)
        {
            _ = excelRange?.Style?.Numberformat ?? throw new ArgumentException(ExceptionMessages.ExcelRangeNull);

            excelRange.Style.Numberformat.Format = "€#,##0.00";
            excelRange.Value = 0;
        }
    }
}
