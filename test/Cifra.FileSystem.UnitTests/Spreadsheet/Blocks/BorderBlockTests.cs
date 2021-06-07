using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;
using System.Collections.Generic;

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
            _spreadsheet = new string[20, 20];
            _spreadsheetWriter = new ArrayStylingSpreadsheetWriter(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithMostRightColumn_DrawsBorderUpUntilMostRightColumn()
        {
            // Arrange
            int headerRow = 1;
            int totalRow = 10;
            int gradeRow = 11;
            int mostRightColumn = 5;
            int expectedAssignmentBottomRow = 5;
            List<int> assignmentBottomRows = new List<int> { expectedAssignmentBottomRow };

            var sut = new BorderBlock(headerRow, assignmentBottomRows, totalRow, gradeRow, mostRightColumn);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            for (int columnIndex = 1; columnIndex < mostRightColumn; columnIndex++)
            {
                _spreadsheet[columnIndex, expectedAssignmentBottomRow].Should().Be("Bottom + Thin");
            }
        }

        [TestMethod]
        public void Write_WithMultipleAssignmentsBottomRows_DrawsBorderAtRow()
        {
            // Arrange
            int headerRow = 1;
            int totalRow = 10;
            int gradeRow = 11;
            int mostRightColumn = 1;
            List<int> assignmentBottomRows = new List<int> { 4, 5, 9 };

            var sut = new BorderBlock(headerRow, assignmentBottomRows, totalRow, gradeRow, mostRightColumn);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            foreach (int assignmentBottomRow in assignmentBottomRows)
            {
                _spreadsheet[1, assignmentBottomRow].Should().Be("Bottom + Thin");
            }
        }

        [TestMethod]
        public void Write_WithMultipleAssignmentsBottomRowsAndMultipleColumns_DrawsBorderAtEachRowAndUpUntilMostRightColumn()
        {
            // Arrange
            int headerRow = 1;
            int totalRow = 10;
            int gradeRow = 11;
            int mostRightColumn = 5;
            List<int> assignmentBottomRows = new List<int> { 2, 5, 9 };

            var sut = new BorderBlock(headerRow, assignmentBottomRows, totalRow, gradeRow, mostRightColumn);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            for (int columnIndex = 1; columnIndex <= mostRightColumn; columnIndex++)
            {
                foreach (int assignmentBottomRow in assignmentBottomRows)
                {
                    _spreadsheet[columnIndex, assignmentBottomRow].Should().Be("Bottom + Thin");
                }
            }
        }
    }
}