using System;
using System.Drawing;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OfficeOpenXml;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Formula;
using SpreadsheetWriter.EPPlus.Extensions;
using SpreadsheetWriter.EPPlus.UnitTests.Builders;
using SpreadsheetWriter.EPPlus.UnitTests.Utilities;

namespace SpreadsheetWriter.EPPlus.UnitTests
{
    [TestClass]
    public class ExcelSpreadsheetWriterTests
    {
        private ExcelSpreadsheetWriter _sut;
        private Fixture _fixture;
        private ExcelWorksheet _worksheet;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _worksheet = ExcelTestBuilder.CreateExcelWorksheet();
            _sut = new ExcelSpreadsheetWriter(_worksheet);
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

        [TestMethod]
        public void MoveDown_WithValidSpreadsheet_ReturnsInstanceOfSut()
        {
            // Arrange

            // Act
            var result = _sut.MoveDown();

            // Assert
            result.Should().Be(_sut);
        }

        [TestMethod]
        public void MoveDown_WithValidSpreadsheet_IncreasesYAxisValue()
        {
            // Arrange

            // Act
            _sut.MoveDown();

            // Assert
            _sut.CurrentPosition.X.Should().Be(1);
            _sut.CurrentPosition.Y.Should().Be(2);
        }

        [TestMethod]
        public void MoveUp_WithValidSpreadsheet_ReturnsInstanceOfSut()
        {
            // Arrange

            // Act
            var result = _sut.MoveUp();

            // Assert
            result.Should().Be(_sut);
        }

        [TestMethod]
        public void MoveUp_WithValidSpreadsheet_DecreasesYAxisValue()
        {
            // Arrange

            // Act
            _sut.MoveUp();

            // Assert
            _sut.CurrentPosition.X.Should().Be(1);
            _sut.CurrentPosition.Y.Should().Be(0);
        }

        [TestMethod]
        public void MoveLeft_WithValidSpreadsheet_ReturnsInstanceOfSut()
        {
            // Arrange

            // Act
            var result = _sut.MoveLeft();

            // Assert
            result.Should().Be(_sut);
        }

        [TestMethod]
        public void MoveLeft_WithValidSpreadsheet_DecreasesXAxisValue()
        {
            // Arrange

            // Act
            _sut.MoveLeft();

            // Assert
            _sut.CurrentPosition.X.Should().Be(0);
            _sut.CurrentPosition.Y.Should().Be(1);
        }

        [TestMethod]
        public void MoveRight_WithValidSpreadsheet_ReturnsInstanceOfSut()
        {
            // Arrange

            // Act
            var result = _sut.MoveRight();

            // Assert
            result.Should().Be(_sut);
        }

        [TestMethod]
        public void MoveRight_WithValidSpreadsheet_IncreasesXAxisValue()
        {
            // Arrange

            // Act
            _sut.MoveRight();

            // Assert
            _sut.CurrentPosition.X.Should().Be(2);
            _sut.CurrentPosition.Y.Should().Be(1);
        }

        [TestMethod]
        public void MoveDownTimes_WithValidNumberOfTimes_ReturnsInstanceOfSut()
        {
            // Arrange
            var times = _fixture.Create<int>();

            // Act
            var result = _sut.MoveDownTimes(times);

            // Assert
            result.Should().Be(_sut);
        }

        [TestMethod]
        public void MoveDownTimes_WithValidNumberOfTimes_IncreasesYAxisValueNumberOfTimes()
        {
            // Arrange
            var times = _fixture.Create<int>();

            // Act
            _sut.MoveDownTimes(times);

            // Assert
            _sut.CurrentPosition.X.Should().Be(1);
            _sut.CurrentPosition.Y.Should().Be(times + 1);
        }

        [TestMethod]
        public void MoveUpTimes_WithValidNumberOfTimes_ReturnsInstanceOfSut()
        {
            // Arrange
            var times = _fixture.Create<int>();

            // Act
            var result = _sut.MoveUpTimes(times);

            // Assert
            result.Should().Be(_sut);
        }

        [TestMethod]
        public void MoveUpTimes_WithValidNumberOfTimes_DecreasesYAxisValueNumberOfTimes()
        {
            // Arrange
            var times = _fixture.Create<int>();

            // Act
            _sut.MoveUpTimes(times);

            // Assert
            _sut.CurrentPosition.X.Should().Be(1);
            _sut.CurrentPosition.Y.Should().Be(-times + 1);
        }

        [TestMethod]
        public void MoveLeftTimes_WithValidNumberOfTimes_ReturnsInstanceOfSut()
        {
            // Arrange
            var times = _fixture.Create<int>();

            // Act
            var result = _sut.MoveLeftTimes(times);

            // Assert
            result.Should().Be(_sut);
        }

        [TestMethod]
        public void MoveLeftTimes_WithValidNumberOfTimes_DecreasesXAxisValueNumberOfTimes()
        {
            // Arrange
            var times = _fixture.Create<int>();

            // Act
            _sut.MoveLeftTimes(times);

            // Assert
            _sut.CurrentPosition.X.Should().Be(-times + 1);
            _sut.CurrentPosition.Y.Should().Be(1);
        }

        [TestMethod]
        public void MoveRightTimes_WithValidNumberOfTimes_ReturnsInstanceOfSut()
        {
            // Arrange
            var times = _fixture.Create<int>();

            // Act
            var result = _sut.MoveLeftTimes(times);

            // Assert
            result.Should().Be(_sut);
        }

        [TestMethod]
        public void MoveRightTimes_WithValidNumberOfTimes_IncreasesXAxisValueNumberOfTimes()
        {
            // Arrange
            var times = _fixture.Create<int>();

            // Act
            _sut.MoveRightTimes(times);

            // Assert
            _sut.CurrentPosition.X.Should().Be(times + 1);
            _sut.CurrentPosition.Y.Should().Be(1);
        }

        [TestMethod]
        public void Write_WithValidValue_ReturnsInstanceOfSut()
        {
            // Arrange
            string value = _fixture.Create<string>();

            // Act
            ISpreadsheetWriter result = _sut.Write(value);

            // Assert
            result.Should().Be(_sut);
        }

        [TestMethod]
        public void Write_WithStringValue_InsertsStringInCurrentCell()
        {
            // Arrange
            string expectedValue = _fixture.Create<string>();

            // Act
            _sut.Write(expectedValue);

            // Assert
            var cell = _worksheet.GetCell(_sut.CurrentPosition);
            cell.Value.Should().Be(expectedValue);
        }

        [TestMethod]
        public void Write_WithDecimalValue_InsertsDecimalInCurrentCell()
        {
            // Arrange
            decimal expectedValue = _fixture.Create<decimal>();

            // Act
            _sut.Write(expectedValue);

            // Assert
            var cell = _worksheet.GetCell(_sut.CurrentPosition);
            cell.Value.Should().Be(expectedValue);
        }


        [TestMethod]
        public void Write_WithNull_ClearsExistingValueInCurrentCell()
        {
            // Arrange
            string expectedValue = null;
            _sut.Write("existing value");

            // Act
            _sut.Write(expectedValue);

            // Assert
            var cell = _worksheet.GetCell(_sut.CurrentPosition);
            cell.Value.Should().Be(expectedValue);
        }

        [TestMethod]
        public void PlaceStandardFormula_WithValidValue_ReturnsInstanceOfSut()
        {
            // Arrange
            var startPosition = new Point(_fixture.Create<short>(), _fixture.Create<short>());
            var endPosition = new Point(_fixture.Create<short>(), _fixture.Create<short>());
            var formulaType = _fixture.Create<FormulaType>();

            // Act
            ISpreadsheetWriter result = _sut.PlaceStandardFormula(startPosition, endPosition, formulaType);

            // Assert
            result.Should().Be(_sut);
        }

        [TestMethod]
        public void PlaceStandardFormula_WithValidPoints_PlacesFormulaInCurrentCell()
        {
            // Arrange
            var startPosition = new Point(_fixture.Create<short>(), _fixture.Create<short>());
            var endPosition = new Point(_fixture.Create<short>(), _fixture.Create<short>());
            var formulaType = _fixture.Create<FormulaType>();

            // Act
            _sut.PlaceStandardFormula(startPosition, endPosition, formulaType);

            // Assert
            var cell = _worksheet.GetCell(_sut.CurrentPosition);
            var startAddress = $"{ExcelColumnUtility.GetExcelColumnName(startPosition.X)}{startPosition.Y}";
            var endAddress = $"{ExcelColumnUtility.GetExcelColumnName(endPosition.X)}{endPosition.Y}";

            cell.Formula.Should().Contain($"={formulaType}({startAddress}:{endAddress})");
        }

        [TestMethod]
        public void PlaceCustomFormula_WithValidValue_ReturnsInstanceOfSut()
        {
            // Arrange
            var expectedFormula = _fixture.Create<string>();
            var formulaBuilder = new Mock<IFormulaBuilder>();
            formulaBuilder.Setup(x => x.Build())
                .Returns(expectedFormula);

            // Act
            ISpreadsheetWriter result = _sut.PlaceCustomFormula(formulaBuilder.Object);

            // Assert
            result.Should().Be(_sut);
        }

        [TestMethod]
        public void PlaceCustomFormula_WithFormulaBuilder_PlacesFormulaToCurrentCell()
        {
            // Arrange
            var expectedFormula = _fixture.Create<string>();
            var formulaBuilder = new Mock<IFormulaBuilder>();
            formulaBuilder.Setup(x => x.Build())
                .Returns(expectedFormula);

            // Act
            ISpreadsheetWriter result = _sut.PlaceCustomFormula(formulaBuilder.Object);

            // Assert
            var cell = _worksheet.GetCell(_sut.CurrentPosition);
            cell.Formula.Should().Be(expectedFormula);
        }
    }
}
