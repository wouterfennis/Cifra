using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using SpreadsheetWriter.EPPlus.UnitTests.Builders;

namespace SpreadsheetWriter.EPPlus.UnitTests
{
    [TestClass]
    public class ExcelRangeWrapperTests
    {
        [TestMethod]
        public void Constructor_WithoutExcelRange_ThrowsException()
        {
            // Arrange
            ExcelRange excelRange = null;

            // Act
            Action action = () => new ExcelRangeWrapper(excelRange);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Address_WithValidExcelRange_ReturnsAddressOfExcelRange()
        {
            // Arrange
            var excelRange = ExcelTestBuilder.CreateExcelRange();
            var sut = new ExcelRangeWrapper(excelRange);

            // Act
            string result = sut.Address;

            // Assert
            result.Should().Be(excelRange.Address);
        }
    }
}
