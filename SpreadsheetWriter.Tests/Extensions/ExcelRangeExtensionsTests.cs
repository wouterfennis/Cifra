using System;
using System.Drawing;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using SpreadsheetWriter.EPPlus.Extensions;

namespace SpreadsheetWriter.EPPlus.UnitTests.Extensions
{
    [TestClass]
    public class ExcelRangeExtensionsTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void SetBackgroundColor_WithExcelRangeNull_ThrowsException()
        {
            // Arrange
            ExcelRange excelRange = null;

            // Act
            Action action = () => excelRange.SetBackgroundColor(_fixture.Create<Color>());

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}