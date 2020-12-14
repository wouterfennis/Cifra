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
    public class TitleBlockTest
    {
        private string[,] _worksheet;
        private Point _startpoint;
        private Fixture _fixture;
        private TestSpreadsheetWriter _spreadsheetWriter;
        private TitleBlock _sut;

        [TestInitialize]
        public void Initialize()
        {
            _worksheet = new string[5, 5];
            _startpoint = new Point(0, 0);
            _fixture = new Fixture();
            _spreadsheetWriter = new TestSpreadsheetWriter(_worksheet);
        }

        [TestMethod]
        public void Write_WithTitle_PutsTitleOnRightPosition()
        {
            // Arrange
            var expectedTitle = _fixture.Create<Name>();
            var expectedCreatedOn = _fixture.Create<DateTime>();
            var titleBlockInput = new TitleBlock.TitleBlockInput(_startpoint, expectedTitle, expectedCreatedOn);
            _sut = new TitleBlock(titleBlockInput);

            // Act
            _sut.Write(_spreadsheetWriter);

            // Assert
            _worksheet[0, 0].Should().Be(expectedTitle.Value);
        }

        [TestMethod]
        public void Write_WithCreatedOn_PutsCreatedOnOnRightPosition()
        {
            // Arrange
            var expectedTitle = _fixture.Create<Name>();
            var expectedCreatedOn = _fixture.Create<DateTime>();
            var titleBlockInput = new TitleBlock.TitleBlockInput(_startpoint, expectedTitle, expectedCreatedOn);
            _sut = new TitleBlock(titleBlockInput);

            // Act
            _sut.Write(_spreadsheetWriter);

            // Assert
            _worksheet[0, 1].Should().Be("Gemaakt op:");
            _worksheet[1, 1].Should().Be(expectedCreatedOn.ToString("dd-MM-yyyy"));
        }
    }
}
