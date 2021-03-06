using System;
using System.Drawing;
using AutoFixture;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class TitleBlockTest
    {
        private string[,] _spreadsheet;
        private Point _startpoint;
        private Fixture _fixture;
        private ArraySpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _spreadsheet = new string[5, 5];
            _startpoint = new Point(0, 0);
            _fixture = new Fixture();
            _spreadsheetWriter = new ArraySpreadsheetWriter(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithTitle_PutsTitleOnRightPosition()
        {
            // Arrange
            var expectedTitle = _fixture.Create<Name>();
            var expectedCreatedOn = _fixture.Create<DateTime>();
            var sut = new TitleBlock(_startpoint, expectedTitle, expectedCreatedOn);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _spreadsheet[0, 0].Should().Be(expectedTitle.Value);
        }

        [TestMethod]
        public void Write_WithCreatedOn_PutsCreatedOnOnRightPosition()
        {
            // Arrange
            var expectedTitle = _fixture.Create<Name>();
            var expectedCreatedOn = _fixture.Create<DateTime>();
            var sut = new TitleBlock(_startpoint, expectedTitle, expectedCreatedOn);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _spreadsheet[0, 1].Should().Be("Gemaakt op:");
            _spreadsheet[1, 1].Should().Be(expectedCreatedOn.ToString("dd-MM-yyyy"));
        }
    }
}
