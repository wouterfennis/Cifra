using AutoFixture;
using Cifra.FileSystem.Spreadsheet.Blocks;
using Cifra.TestUtilities.SpreadsheetWriter;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpreadsheetWriter.Abstractions.Formula;
using SpreadsheetWriter.Test;
using System.Drawing;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class GradesBlockTests
    {
        private string[,] _spreadsheet;
        private Point _startpoint;
        private Fixture _fixture;
        private Mock<IFormulaBuilderFactory> _formulaBuilderFactory;
        private ArrayContentSpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _spreadsheet = new string[10, 10];
            _startpoint = new Point(0, 5);
            _fixture = new Fixture();
            _formulaBuilderFactory = new Mock<IFormulaBuilderFactory>();
            _spreadsheetWriter = new ArrayContentSpreadsheetWriter(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithAssignments_PutsTitleOnRightPosition()
        {
            // Arrange
            int achievedScoreRow = 5;
            int scoresStartColumn = 1;
            var maximumScorePosition = new Point(1, 0);
            var standardizationFactorPosition = new Point(1, 1);
            var minimumScorePosition = new Point(1, 2);
            int numberOfStudents = 1;
            var expectedFormula = _fixture.Create<string>();

            SetupFormulaBuilderFactory(_formulaBuilderFactory, expectedFormula);

            var sut = new GradesBlock(
                _startpoint,
                _formulaBuilderFactory.Object,
                achievedScoreRow,
                scoresStartColumn,
                maximumScorePosition,
                standardizationFactorPosition,
                minimumScorePosition,
                numberOfStudents);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _spreadsheet[0, 5].Should().Be("Cijfer");
        }

        [TestMethod]
        public void Write_WithScores_PutsForumulaOnRightPositions()
        {
            // Arrange
            int achievedScoreRow = 5;
            int scoresStartColumn = 1;
            var maximumScorePosition = new Point(1, 0);
            var standardizationFactorPosition = new Point(1, 1);
            var minimumScorePosition = new Point(1, 2);
            int numberOfStudents = 1;
            var expectedFormula = _fixture.Create<string>();

            SetupFormulaBuilderFactory(_formulaBuilderFactory, expectedFormula);

            var sut = new GradesBlock(
                _startpoint,
                _formulaBuilderFactory.Object,
                achievedScoreRow,
                scoresStartColumn,
                maximumScorePosition,
                standardizationFactorPosition,
                minimumScorePosition,
                numberOfStudents);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _spreadsheet[1, 5].Should().Be(expectedFormula);
            _spreadsheet[2, 5].Should().Be(expectedFormula);
        }

        private void SetupFormulaBuilderFactory(Mock<IFormulaBuilderFactory> formulaBuilderFactory, string expectedFormula)
        {
            var formulaBuilder = new Mock<IFormulaBuilder>();
            FormulaBuilderTestUtilities.SetupFormulaBuilder(formulaBuilder, expectedFormula);

            formulaBuilderFactory.Setup(x => x.Create())
                .Returns(formulaBuilder.Object);
        }
    }
}