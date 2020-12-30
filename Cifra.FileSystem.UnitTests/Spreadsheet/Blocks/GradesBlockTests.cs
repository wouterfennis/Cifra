using AutoFixture;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Test;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class GradesBlockTests
    {
        private string[,] _worksheet;
        private Point _startpoint;
        private Fixture _fixture;
        private Mock<IFormulaBuilder> _formulaBuilder;
        private ArraySpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _worksheet = new string[10, 10];
            _startpoint = new Point(0, 5);
            _fixture = new Fixture();
            _formulaBuilder = new Mock<IFormulaBuilder>();
            _spreadsheetWriter = new ArraySpreadsheetWriter(_worksheet);
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

            SetupFormulaBuilder(_formulaBuilder, expectedFormula);

            var totalPointsBlockInput = new GradesBlock.GradesBlockInput(
                _startpoint,
                _formulaBuilder.Object,
                achievedScoreRow,
                scoresStartColumn,
                maximumScorePosition,
                standardizationFactorPosition,
                minimumScorePosition,
                numberOfStudents);
            var sut = new GradesBlock(totalPointsBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _worksheet[0, 5].Should().Be("Cijfer");
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

            SetupFormulaBuilder(_formulaBuilder, expectedFormula);

            var totalPointsBlockInput = new GradesBlock.GradesBlockInput(
                _startpoint,
                _formulaBuilder.Object,
                achievedScoreRow,
                scoresStartColumn,
                maximumScorePosition,
                standardizationFactorPosition,
                minimumScorePosition,
                numberOfStudents);
            var sut = new GradesBlock(totalPointsBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _worksheet[1, 5].Should().Be(expectedFormula);
            _worksheet[2, 5].Should().Be(expectedFormula);
        }

        private void SetupFormulaBuilder(Mock<IFormulaBuilder> formulaBuilder, string expectedFormula)
        {
            formulaBuilder.Setup(x => x.AddCellAddress(It.IsAny<string>()))
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddClosedParenthesis())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddDivideSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddEqualsSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddMultiplySign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddOpenParenthesis())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddSubtractSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddSumSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddSumSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.Build())
                .Returns(expectedFormula);
        }
    }
}