//using System.Drawing;
//using System.IO;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using OfficeOpenXml;
//using SpreadsheetWriter.EPPlus.Extensions;

//namespace SpreadsheetWriter.IntegrationTests.Extensions
//{
//    [TestClass]
//    [TestCategory("Integration")]
//    public class ExcelRangeExtensionsTests
//    {
//        [TestMethod]
//        public void SetBackgroundColor_WithInvalidObject_ThrowsException()
//        {
//            // Arrange
//            var filePath = Path.Combine(Path.GetTempPath(), "spreadsheet.xsls");
//            DeleteFileIfExists(filePath);

//            var excelPackage = new ExcelPackage();
//            ExcelWorksheet worksheet =  excelPackage.Workbook.Worksheets.Add("test");
//            var cell = worksheet.Cells;
//            cell.Style = null;
//            // Act
//            worksheet.Cells.SetBackgroundColor(Color.White);

//            // Assert
//        }

//        private void DeleteFileIfExists(string filePath)
//        {
//            if (File.Exists(filePath))
//            {
//                File.Delete(filePath);
//            }
//            if (File.Exists(filePath))
//            {
//                throw new InternalTestFailureException($"The file: {filePath} still exists after trying to delete it.");
//            }
//        }
//    }
//}
