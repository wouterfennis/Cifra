using System;
using System.Drawing;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using SpreadsheetWriter.EPPlus;
using SpreadsheetWriter.UnitTests.Builders;

namespace SpreadsheetWriter.UnitTests
{
    [TestClass]
    public class ExcelSpreadsheetWriterTests
    {
        private ExcelSpreadsheetWriter _sut;
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            var worksheet = ExcelTestBuilder.CreateExcelWorksheet();
            _sut = new ExcelSpreadsheetWriter(worksheet);
        }

        [TestMethod]
        public void Constructor_WithoutWorksheet_ThrowsException()
        {
            // Arrange
            ExcelWorksheet worksheet = null;

            // Act
            Action action = () => new ExcelSpreadsheetWriter(worksheet);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_WithValidWorksheet_SetsCurrentCellToTopLeft()
        {
            // Arrange
            var worksheet = ExcelTestBuilder.CreateExcelWorksheet();

            // Act
            _sut = new ExcelSpreadsheetWriter(worksheet);

            // Assert
            _sut.CurrentPosition.X.Should().Be(1);
            _sut.CurrentPosition.Y.Should().Be(1);
        }

        [TestMethod]
        public void GetCellRange_WithValidPoint_ReturnsMatchingExcelRange()
        {
            // Arrange
            var point = new Point(_fixture.Create<short>(), _fixture.Create<short>());

            // Act
            var result = _sut.GetCellRange(point);

            // Assert
            result.Should().NotBeNull();
            result.Address.Should().Contain(point.Y.ToString());
        }
    }
}
