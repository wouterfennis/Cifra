using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AutoFixture;
using Cifra.Application.Models.Test;
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
        private Fixture _fixture;
        private ArraySpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _spreadsheet = new string[10, 10];
            _startpoint = new Point(0, 0);
            _fixture = new Fixture();
            _spreadsheetWriter = new ArraySpreadsheetWriter(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithAssignments_PutsHeaderOnRightPosition()
        {
            // Arrange
            int expectedQuestionNamesColumns = 2;
            var assignments = new List<Assignment> { };
            var questionsBlockInput = new AssignmentsBlock.AssignmentsBlockInput(_startpoint, assignments, expectedQuestionNamesColumns);
            var sut = new AssignmentsBlock(questionsBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _spreadsheet[0, 0].Should().Be("Opgave");
            _spreadsheet[expectedQuestionNamesColumns, 0].Should().Be("Punten");

            SpreadsheetTestUtilities.PrintArraySpreadsheet(_spreadsheet);
        }

        [TestMethod]
        public void Write_WithAssignment_SavesStartRowOfAssignment()
        {
            // Arrange
            int questionNamesColumns = 2;
            int numberOfQuestions = 3;
            var assignments = new List<Assignment> {
                new Assignment(numberOfQuestions)
            };
            var assignmentsBlockInput = new AssignmentsBlock.AssignmentsBlockInput(_startpoint, assignments, questionNamesColumns);
            var sut = new AssignmentsBlock(assignmentsBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            sut.AssignmentStartRows.Should().ContainSingle();
            var actualStartRow = sut.AssignmentStartRows.Single();
            actualStartRow.Should().Be(1);
        }

        [TestMethod]
        public void Write_WithAssignments_SavesStartRowOfEachAssignment()
        {
            // Arrange
            int questionNamesColumns = 2;
            int numberOfQuestions = 3;
            var assignments = new List<Assignment> {
                new Assignment(numberOfQuestions),
                new Assignment(0)
            };
            var assignmentsBlockInput = new AssignmentsBlock.AssignmentsBlockInput(_startpoint, assignments, questionNamesColumns);
            var sut = new AssignmentsBlock(assignmentsBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            int headerOffset = 1;
            sut.AssignmentStartRows.Should().HaveCount(assignments.Count);
            sut.AssignmentStartRows.Should().HaveCount(assignments.Count);
            int firstAssignmentRow = sut.AssignmentStartRows.ElementAt(0);
            firstAssignmentRow.Should().Be(headerOffset);

            int secondAssignmentRow = sut.AssignmentStartRows.ElementAt(1);
            secondAssignmentRow.Should().Be(headerOffset + numberOfQuestions);
        }
    }
}
