using OfficeOpenXml;

namespace SpreadsheetWriter.EPPlus.UnitTests.Builders
{
    public static class ExcelTestBuilder
    {
        public static ExcelRange CreateExcelRange()
        {
            var worksheet = CreateExcelWorksheet();
            return worksheet.Cells;
        }

        public static ExcelWorksheet CreateExcelWorksheet()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelPackage = new ExcelPackage();
            return excelPackage.Workbook.Worksheets.Add("test");
        }
    }
}
