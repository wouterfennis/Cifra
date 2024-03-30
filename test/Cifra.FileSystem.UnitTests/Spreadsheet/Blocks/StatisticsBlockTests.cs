using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;
using System.Drawing;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class StatisticsBlockTests
    {
        private string[,] _spreadsheet;
        private Point _startpoint;
        private Point _satisfactoryGradePoint;
        private ArrayContentSpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _spreadsheet = new string[10, 10];
            _startpoint = new Point(0, 3);
            _satisfactoryGradePoint = new Point(0, 2);
            _spreadsheetWriter = new ArrayContentSpreadsheetWriter(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithAverages_PutsAveragePointsTitleOnRightPosition()
        {
            // Arrange
            var sut = new StatisticsBlock(
                _startpoint,
                4,
                5,
                6,
                3,
                _satisfactoryGradePoint);

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
                4,
                5,
                6,
                3,
                _satisfactoryGradePoint);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            SpreadsheetTestUtilities.PrintArraySpreadsheet(_spreadsheet);
            _spreadsheet[0, 4].Should().Be("Gemiddelde cijfer");
        }

        [TestMethod]
        public void Write_WithAverages_PutsNumberOfBadGradesTitleOnRightPosition()
        {
            // Arrange
            var sut = new StatisticsBlock(
                _startpoint,
                4,
                5,
                6,
                3,
                _satisfactoryGradePoint);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            SpreadsheetTestUtilities.PrintArraySpreadsheet(_spreadsheet);
            _spreadsheet[0, 5].Should().Be("Aantal onvoldoendes");
        }

        [TestMethod]
        public void Write_WithAverages_PutsPercentageOfBadGradesTitleOnRightPosition()
        {
            // Arrange
            var sut = new StatisticsBlock(
                _startpoint,
                4,
                5,
                6,
                3,
                _satisfactoryGradePoint);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            SpreadsheetTestUtilities.PrintArraySpreadsheet(_spreadsheet);
            _spreadsheet[0, 6].Should().Be("Percentage onvoldoendes van totaal");
        }
    }
}
