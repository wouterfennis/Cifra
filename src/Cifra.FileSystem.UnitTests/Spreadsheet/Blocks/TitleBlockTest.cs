using AutoFixture;
using Cifra.Core.Models.ValueTypes;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;
using System;
using System.Drawing;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class TitleBlockTest
    {
        private string[,] _spreadsheet;
        private Point _startpoint;
        private Fixture _fixture;
        private ArrayContentSpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _spreadsheet = new string[5, 5];
            _startpoint = new Point(0, 0);
            _fixture = new Fixture();
            _spreadsheetWriter = new ArrayContentSpreadsheetWriter(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithTitle_PutsTitleOnRightPosition()
        {
            // Arrange
            var expectedTitle = _fixture.Create<Name>();
            var expectedCreatedOn = _fixture.Create<DateTime>();
            var expectedApplicationVersion = _fixture.Create<string>();
            var sut = new TitleBlock(_startpoint, expectedTitle, expectedCreatedOn, expectedApplicationVersion);

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
            var expectedApplicationVersion = _fixture.Create<string>();
            var sut = new TitleBlock(_startpoint, expectedTitle, expectedCreatedOn, expectedApplicationVersion);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _spreadsheet[0, 1].Should().Be("Gemaakt op:");
            _spreadsheet[1, 1].Should().Be(expectedCreatedOn.ToString("dd-MM-yyyy"));
        }

        [TestMethod]
        public void Write_WithApplicationVersion_PutsApplicationVersionOnRightPosition()
        {
            // Arrange
            var expectedTitle = _fixture.Create<Name>();
            var expectedCreatedOn = _fixture.Create<DateTime>();
            var expectedApplicationVersion = _fixture.Create<string>();
            var sut = new TitleBlock(_startpoint, expectedTitle, expectedCreatedOn, expectedApplicationVersion);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _spreadsheet[0, 2].Should().Be("Cifra:");
            _spreadsheet[1, 2].Should().Be(expectedApplicationVersion);
        }
    }
}
