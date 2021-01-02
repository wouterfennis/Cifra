using AutoFixture;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;
using System;
using System.Drawing;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class AveragesBlockTests
    {
        private string[,] _worksheet;
        private Point _startpoint;
        private Fixture _fixture;
        private ArraySpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _worksheet = new string[10, 5];
            _startpoint = new Point(0, 3);
            _fixture = new Fixture();
            _spreadsheetWriter = new ArraySpreadsheetWriter(_worksheet);
        }

        [TestMethod]
        public void Write_WithAverages_PutsAveragePointsTitleOnRightPosition()
        {
            // Arrange
            var averagesBlockInput = new AveragesBlock.AveragesBlockInput(
                _startpoint,
                0,
                1,
                2,
                3);
            var sut = new AveragesBlock(averagesBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            SpreadsheetTestUtilities.PrintArrayWorksheet(_worksheet);
            _worksheet[0, 3].Should().Be("Gemiddelde aantal punten");
        }

        [TestMethod]
        public void Write_WithAverages_PutsAverageGradeTitleOnRightPosition()
        {
            // Arrange
            var averagesBlockInput = new AveragesBlock.AveragesBlockInput(
                _startpoint,
                0,
                1,
                2,
                3);
            var sut = new AveragesBlock(averagesBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            SpreadsheetTestUtilities.PrintArrayWorksheet(_worksheet);
            _worksheet[0, 4].Should().Be("Gemiddelde cijfer");
        }
    }
}
