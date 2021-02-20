using System;
using System.Drawing;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using SpreadsheetWriter.EPPlus.Extensions;

namespace SpreadsheetWriter.UnitTests.Extensions
{
    [TestClass]
    public class ExcelWorksheetExtensionsTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void GetCell_WithExcelRangeNull_ThrowsException()
        {
            // Arrange
            ExcelWorksheet excelWorksheet = null;

            // Act
            Action action = () => excelWorksheet.GetCell(new Point());

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}
