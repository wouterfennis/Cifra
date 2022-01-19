using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Cifra.Core.Models.Test;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class AssignmentsBlockTest
    {
        private string[,] _spreadsheet;
        private Point _startpoint;
        private ArrayContentSpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _spreadsheet = new string[10, 10];
            _startpoint = new Point(0, 0);
            _spreadsheetWriter = new ArrayContentSpreadsheetWriter(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithAssignments_PutsHeaderOnRightPosition()
        {
            // Arrange
            int expectedQuestionNamesColumns = 2;
            var assignments = new List<Assignment> { };
            var sut = new AssignmentsBlock(_startpoint, assignments, expectedQuestionNamesColumns);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _spreadsheet[0, 0].Should().Be("Opgave");
            _spreadsheet[expectedQuestionNamesColumns, 0].Should().Be("Punten");

            SpreadsheetTestUtilities.PrintArraySpreadsheet(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithAssignment_SavesLastRowOfAssignment()
        {
            // Arrange
            int questionNamesColumns = 2;
            int numberOfQuestions = 3;
            var assignments = new List<Assignment> {
                new Assignment(numberOfQuestions)
            };
            var sut = new AssignmentsBlock(_startpoint, assignments, questionNamesColumns);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            sut.AssignmentBottomRows.Should().ContainSingle();
            var actualStartRow = sut.AssignmentBottomRows.Single();
            actualStartRow.Should().Be(3);
        }

        [TestMethod]
        public void Write_WithAssignments_SavesLastRowOfEachAssignment()
        {
            // Arrange
            int questionNamesColumns = 2;
            var assignments = new List<Assignment> {
                new Assignment(3),
                new Assignment(1)
            };
            var sut = new AssignmentsBlock(_startpoint, assignments, questionNamesColumns);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            int headerOffset = 1;
            sut.AssignmentBottomRows.Should().HaveCount(assignments.Count);
            sut.AssignmentBottomRows.Should().HaveCount(assignments.Count);
            int firstAssignmentRow = sut.AssignmentBottomRows.ElementAt(0);
            firstAssignmentRow.Should().Be(headerOffset + 2);

            int secondAssignmentRow = sut.AssignmentBottomRows.ElementAt(1);
            secondAssignmentRow.Should().Be(headerOffset + 3);
        }
    }
}
