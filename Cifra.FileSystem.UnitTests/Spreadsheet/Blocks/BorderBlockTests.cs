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
            for (int columnIndex = 1; columnIndex <= mostRightColumn; columnIndex++)
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
            List<int> assignmentBottomRows = new List<int> { 1, 5, 9 };

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

        [TestMethod]
        public void Write_WithHeaderRow_DrawsBorderOnHeaderRow()
        {
            // Arrange
            int headerRow = 1;
            int totalRow = 10;
            int gradeRow = 11;
            int mostRightColumn = 5;
            List<int> assignmentBottomRows = new List<int> { 4 };

            var sut = new BorderBlock(headerRow, assignmentBottomRows, totalRow, gradeRow, mostRightColumn);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            for (int columnIndex = 1; columnIndex <= mostRightColumn; columnIndex++)
            {
                _spreadsheet[columnIndex, headerRow].Should().Be("Bottom + Thin");
            }
        }

        [TestMethod]
        public void Write_WithTotalRow_DrawsBorderOnTotalRow()
        {
            // Arrange
            int headerRow = 1;
            int totalRow = 10;
            int gradeRow = 11;
            int mostRightColumn = 5;
            List<int> assignmentBottomRows = new List<int> { 4 };

            var sut = new BorderBlock(headerRow, assignmentBottomRows, totalRow, gradeRow, mostRightColumn);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            for (int columnIndex = 1; columnIndex <= mostRightColumn; columnIndex++)
            {
                _spreadsheet[columnIndex, totalRow].Should().Be("Bottom + Double");
            }
        }

        [TestMethod]
        public void Write_WithGradeRow_DrawsBorderOnGradeRow()
        {
            // Arrange
            int headerRow = 1;
            int totalRow = 10;
            int gradeRow = 11;
            int mostRightColumn = 5;
            List<int> assignmentBottomRows = new List<int> { 4 };

            var sut = new BorderBlock(headerRow, assignmentBottomRows, totalRow, gradeRow, mostRightColumn);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            for (int columnIndex = 1; columnIndex <= mostRightColumn; columnIndex++)
            {
                _spreadsheet[columnIndex, gradeRow].Should().Be("Bottom + Double");
            }
        }
    }
}