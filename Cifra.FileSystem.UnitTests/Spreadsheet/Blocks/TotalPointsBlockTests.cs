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
        private string[,] _worksheet;
        private Point _startpoint;
        private Fixture _fixture;
        private ArraySpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _worksheet = new string[5, 5];
            _startpoint = new Point(0, 5);
            _fixture = new Fixture();
            _spreadsheetWriter = new ArraySpreadsheetWriter(_worksheet);
        }

        [TestMethod]
        public void Write_WithAssignements_PutsTitleOnRightPosition()
        {
            // Arrange
            var firstScorePoint = new Point(1, 0);
            var totalPointsBlockInput = new TotalPointsBlock.TotalPointsBlockInput(_startpoint, firstScorePoint);
            var sut = new TotalPointsBlock(totalPointsBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _worksheet[0, 5].Should().Be("Totaal");
        }
    }
}