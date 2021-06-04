using AutoFixture;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;
using System.Drawing;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class AveragesBlockTests
    {
        private string[,] _spreadsheet;
        private Point _startpoint;
        private Fixture _fixture;
        private ArrayContentSpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _spreadsheet = new string[10, 5];
            _startpoint = new Point(0, 3);
            _fixture = new Fixture();
            _spreadsheetWriter = new ArrayContentSpreadsheetWriter(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithAverages_PutsAveragePointsTitleOnRightPosition()
        {
            // Arrange
            var sut = new StatisticsBlock(
                _startpoint,
                0,
                1,
                2,
                3);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            SpreadsheetTestUtilities.PrintArraySpreadsheet(_spreadsheet);
            _spreadsheet[0, 3].Should().Be("Gemiddelde aantal punten");
        }

        [TestMethod]
        public void Write_WithAverages_PutsAverageGradeTitleOnRightPosition()
        {
            // Arrange
            var sut = new StatisticsBlock(
                _startpoint,
                0,
                1,
                2,
                3);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            SpreadsheetTestUtilities.PrintArraySpreadsheet(_spreadsheet);
            _spreadsheet[0, 4].Should().Be("Gemiddelde cijfer");
        }
    }
}
