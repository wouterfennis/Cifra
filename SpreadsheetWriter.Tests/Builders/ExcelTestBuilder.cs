using OfficeOpenXml;

namespace SpreadsheetWriter.UnitTests.Builders
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
            var excelPackage = new ExcelPackage();
            return excelPackage.Workbook.Worksheets.Add("test");
        }
    }
}
