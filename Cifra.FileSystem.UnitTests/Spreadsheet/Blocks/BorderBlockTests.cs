using System.Collections.Generic;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class BorderBlockTests
    {
        private string[,] _spreadsheet;
        private ArrayStylingSpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _spreadsheet = new string[10, 10];
            _spreadsheetWriter = new ArrayStylingSpreadsheetWriter(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithMostRightColumn_DrawsBorderUpUntil()
        {
            // Arrange
            int mostRightColumn = 5;
            int expectedAssignmentBottomRow = 1;
            List<int> assignmentBottomRows = new List<int> { expectedAssignmentBottomRow };

            var sut = new BorderBlock(assignmentBottomRows, mostRightColumn);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            for (int columnIndex = 0; columnIndex < mostRightColumn; columnIndex++)
            {
                _spreadsheet[columnIndex, 1].Should().Be("Bottom + Medium");
            }
        }

        [TestMethod]
        public void Write_WithMultipleAssignmentsBottomRows_DrawsBorderAtRow()
        {
            // Arrange
            int mostRightColumn = 1;
            List<int> assignmentBottomRows = new List<int> { 1, 5, 9 };

            var sut = new BorderBlock(assignmentBottomRows, mostRightColumn);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            foreach (int assignmentBottomRow in assignmentBottomRows)
            {
                _spreadsheet[0, assignmentBottomRow].Should().Be("Bottom + Medium");
            }
        }

        [TestMethod]
        public void Write_WithMultipleAssignmentsBottomRowsAndMultipleColumns_DrawsBorderAtEachRowAndUpUntilMostRightColumn()
        {
            // Arrange
            int mostRightColumn = 5;
            List<int> assignmentBottomRows = new List<int> { 1, 5, 9 };

            var sut = new BorderBlock(assignmentBottomRows, mostRightColumn);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            for (int columnIndex = 0; columnIndex < mostRightColumn; columnIndex++)
            {
                foreach (int assignmentBottomRow in assignmentBottomRows)
                {
                    _spreadsheet[columnIndex, assignmentBottomRow].Should().Be("Bottom + Medium");
                }
            }
        }
    }
}