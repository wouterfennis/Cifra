using AutoFixture;
using Cifra.Application.Models.Test;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
            var assignments = new List<Assignment> { CreateAssignment() };
            var questionsBlockInput = new AssignmentsBlock.AssignmentsBlockInput(_startpoint, assignments);
            var sut = new AssignmentsBlock(questionsBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            _spreadsheet[0, 0].Should().Be("Opgave");
            _spreadsheet[expectedQuestionNamesColumns + 1, 0].Should().Be("Punten");
        }

        [TestMethod]
        public void Write_WithMultipleQuestionNames_PutsQuestionNamesInGivenOrder()
        {
            // Arrange
            var assignment = CreateAssignment();
            var expectedQuestions = assignment.Questions;
            var assignments = new List<Assignment> { assignment };
            var questionsBlockInput = new AssignmentsBlock.AssignmentsBlockInput(_startpoint, assignments);
            var sut = new AssignmentsBlock(questionsBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            var headerOffset = 1;
            var question = expectedQuestions.First();
            for (int x = 0; x < question.QuestionNames.Count(); x++)
            {
                var questionName = question.QuestionNames.ElementAt(x);
                _spreadsheet[x, headerOffset].Should().Be(questionName.Value);
            }
        }

        [TestMethod]
        public void Write_WithMultipleQuestions_PutsEachQuestionOnNewLine()
        {
            // Arrange
            var assignment = CreateAssignment();
            var expectedQuestions = assignment.Questions;
            var assignments = new List<Assignment> { assignment };
            var questionsBlockInput = new AssignmentsBlock.AssignmentsBlockInput(_startpoint, assignments);
            var sut = new AssignmentsBlock(questionsBlockInput);

            // Act
            sut.Write(_spreadsheetWriter);

            // Assert
            var firstQuestion = expectedQuestions.ElementAt(0);
            var firstQuestionName = firstQuestion.QuestionNames.First();
            _spreadsheet[0, 1].Should().Be(firstQuestionName.Value);
            var secondQuestion = expectedQuestions.ElementAt(1);
            var secondQuestionName = secondQuestion.QuestionNames.First();
            _spreadsheet[0, 2].Should().Be(secondQuestionName.Value);
        }

        private Assignment CreateAssignment()
        {
            var questions = _fixture.CreateMany<Question>()
                .ToList();
            return new Assignment(_fixture.Create<Guid>(), questions);
        }
    }
}
