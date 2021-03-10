using AutoFixture;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpreadsheetWriter.Abstractions;
using SpreadsheetWriter.Abstractions.Formula;
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

            var totalPointsBlockInput = new GradesBlock.GradesBlockInput(
                _startpoint,
                _formulaBuilderFactory.Object,
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

            var totalPointsBlockInput = new GradesBlock.GradesBlockInput(
                _startpoint,
                _formulaBuilderFactory.Object,
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
            _spreadsheet[1, 5].Should().Be(expectedFormula);
            _spreadsheet[2, 5].Should().Be(expectedFormula);
        }

        private void SetupFormulaBuilderFactory(Mock<IFormulaBuilderFactory> formulaBuilderFactory, string expectedFormula)
        {
            var formulaBuilder = new Mock<IFormulaBuilder>();

            formulaBuilder.Setup(x => x.AddCellAddress(It.IsAny<string>()))
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddClosingParenthesis())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddDivisionSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddEqualsSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddMultiplicationSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddOpenParenthesis())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddSubtractionSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddSummationSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.AddSummationSign())
                .Returns(formulaBuilder.Object);

            formulaBuilder.Setup(x => x.Build())
                .Returns(expectedFormula);

            formulaBuilderFactory.Setup(x => x.Create())
                .Returns(formulaBuilder.Object);
        }
    }
}