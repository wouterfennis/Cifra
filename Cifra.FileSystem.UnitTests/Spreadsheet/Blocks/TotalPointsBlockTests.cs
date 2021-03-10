using AutoFixture;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class TotalPointsBlockTests
    {
        private string[,] _spreadsheet;
        private Point _startpoint;
        private Fixture _fixture;
        private ArrayContentSpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _spreadsheet = new string[10, 10];
            _startpoint = new Point(0, 5);
            _fixture = new Fixture();
            _spreadsheetWriter = new ArrayContentSpreadsheetWriter(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithAssignments_PutsTitleOnRightPosition()
        {
            // Arrange
            var scoreTopPoint = new Point(1, 0);
            int numberOfStudents = 1;
            var totalPointsBlockInput = new TotalScoresBlock.TotalScoresBlockInput(
                _startpoint, 
                scoreTopPoint, 
                numberOfStudents);
            var sut = new TotalScoresBlock(totalPointsBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _spreadsheet[0, 5].Should().Be("Totaal");
        }

        [TestMethod]
        public void Write_WithAssignments_PutsSUMFormulaOnMaximumPointsOfAllAssignments()
        {
            // Arrange
            var scoreTopPoint = new Point(1, 0);
            int numberOfStudents = 1;
            var totalPointsBlockInput = new TotalScoresBlock.TotalScoresBlockInput(
                _startpoint, 
                scoreTopPoint,
                numberOfStudents);
            var sut = new TotalScoresBlock(totalPointsBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            SpreadsheetTestUtilities.PrintArraySpreadsheet(_spreadsheet);
            _spreadsheet[1, 0].Should().Be("StartStandardFormulaSUM");
            _spreadsheet[1, 4].Should().Be("EndStandardFormulaSUM");
            _spreadsheet[1, 5].Should().Be("Result of StandardFormulaSUM");
        }

        [TestMethod]
        public void Write_WithMultipleStudents_PutsSUMFormulaOnAllScoresOfStudents()
        {
            // Arrange
            var scoreTopPoint = new Point(1, 0);
            int numberOfStudents = 3;
            var totalPointsBlockInput = new TotalScoresBlock.TotalScoresBlockInput(
                _startpoint,
                scoreTopPoint,
                numberOfStudents);
            var sut = new TotalScoresBlock(totalPointsBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            SpreadsheetTestUtilities.PrintArraySpreadsheet(_spreadsheet);
            _spreadsheet[1, 0].Should().Be("StartStandardFormulaSUM");
            _spreadsheet[1, 4].Should().Be("EndStandardFormulaSUM");
            _spreadsheet[1, 5].Should().Be("Result of StandardFormulaSUM");

            _spreadsheet[2, 0].Should().Be("StartStandardFormulaSUM");
            _spreadsheet[2, 4].Should().Be("EndStandardFormulaSUM");
            _spreadsheet[2, 5].Should().Be("Result of StandardFormulaSUM");

            _spreadsheet[3, 0].Should().Be("StartStandardFormulaSUM");
            _spreadsheet[3, 4].Should().Be("EndStandardFormulaSUM");
            _spreadsheet[3, 5].Should().Be("Result of StandardFormulaSUM");

            _spreadsheet[4, 0].Should().Be("StartStandardFormulaSUM");
            _spreadsheet[4, 4].Should().Be("EndStandardFormulaSUM");
            _spreadsheet[4, 5].Should().Be("Result of StandardFormulaSUM");
        }
    }
}