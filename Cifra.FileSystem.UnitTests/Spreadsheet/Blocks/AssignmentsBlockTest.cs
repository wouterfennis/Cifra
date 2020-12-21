using AutoFixture;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using Cifra.FileSystem.Spreadsheet.Blocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetWriter.Test;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Cifra.FileSystem.UnitTests.Spreadsheet.Blocks
{
    [TestClass]
    public class AssignmentsBlockTest
    {
        private string[,] _worksheet;
        private Point _startpoint;
        private Fixture _fixture;
        private ArraySpreadsheetWriter _spreadsheetWriter;

        [TestInitialize]
        public void Initialize()
        {
            _worksheet = new string[10, 10];
            _startpoint = new Point(0, 0);
            _fixture = new Fixture();
            _spreadsheetWriter = new ArraySpreadsheetWriter(_worksheet);
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
            WorksheetTestUtilities.PrintArrayWorksheet(_worksheet);
            _worksheet[0, 0].Should().Be("Opgave");
            _worksheet[expectedQuestionNamesColumns + 1, 0].Should().Be("Punten");
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
            WorksheetTestUtilities.PrintArrayWorksheet(_worksheet);
            var headerOffset = 1;
            for (int y = 0; y < expectedQuestions.Count(); y++)
            {
                var question = expectedQuestions.ElementAt(y);
                for (int x = 0; x < question.QuestionNames.Count(); x++)
                {
                    var questionName = question.QuestionNames.ElementAt(x);
                    _worksheet[x, y + headerOffset].Should().Be(questionName.Value);
                }
            }
        }

        private Assignment CreateAssignment()
        {
            var questions = _fixture.CreateMany<Question>()
                .ToList();
            return new Assignment(_fixture.Create<Guid>(), questions);
        }

        //[TestMethod]
        //public void Write_WithMaximumPoints_PutsDataOnRightPosition()
        //{
        //    // Arrange
        //    var expectedMaximumPoints = _fixture.Create<decimal>();
        //    var standardizationFactor = _fixture.Create<StandardizationFactor>();
        //    var minimumGrade = Grade.CreateFromByte(1);
        //    var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, expectedMaximumPoints, standardizationFactor, minimumGrade);
        //    var sut = new ConfigurationBlock(configurationBlockInput);

        //    // Act
        //    sut.Write(_spreadsheetWriter);

        //    // Assert
        //    WorksheetTestUtilities.PrintArrayWorksheet(_worksheet);
        //    _worksheet[0, 3].Should().Be("Maximale punten");
        //    _worksheet[1, 3].Should().Be(expectedMaximumPoints.ToString());
        //}

        //[TestMethod]
        //public void Write_WithMaximumPoints_SavesPositionOfMaximumPoints()
        //{
        //    // Arrange
        //    var maximumPoints = _fixture.Create<decimal>();
        //    var standardizationFactor = _fixture.Create<StandardizationFactor>();
        //    var minimumGrade = Grade.CreateFromByte(1);
        //    var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, maximumPoints, standardizationFactor, minimumGrade);
        //    var sut = new ConfigurationBlock(configurationBlockInput);

        //    // Act
        //    sut.Write(_spreadsheetWriter);

        //    // Assert
        //    sut.MaximumPointsPosition.X.Should().Be(1);
        //    sut.MaximumPointsPosition.Y.Should().Be(3);
        //}

        //[TestMethod]
        //public void Write_WithStandardizationFactor_PutsDataOnRightPosition()
        //{
        //    // Arrange
        //    var maximumPoints = _fixture.Create<decimal>();
        //    var expectedStandardizationFactor = _fixture.Create<StandardizationFactor>();
        //    var minimumGrade = Grade.CreateFromByte(1);
        //    var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, maximumPoints, expectedStandardizationFactor, minimumGrade);
        //    var sut = new ConfigurationBlock(configurationBlockInput);

        //    // Act
        //    sut.Write(_spreadsheetWriter);

        //    // Assert
        //    _worksheet[0, 4].Should().Be("Normering");
        //    _worksheet[1, 4].Should().Be(expectedStandardizationFactor.Value.ToString());
        //}

        //[TestMethod]
        //public void Write_WithStandardizationFactor_SavesPositionOfStandardizationFactor()
        //{
        //    // Arrange
        //    var maximumPoints = _fixture.Create<decimal>();
        //    var standardizationFactor = _fixture.Create<StandardizationFactor>();
        //    var minimumGrade = Grade.CreateFromByte(1);
        //    var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, maximumPoints, standardizationFactor, minimumGrade);
        //    var sut = new ConfigurationBlock(configurationBlockInput);

        //    // Act
        //    sut.Write(_spreadsheetWriter);

        //    // Assert
        //    sut.StandardizationfactorPosition.X.Should().Be(1);
        //    sut.StandardizationfactorPosition.Y.Should().Be(4);
        //}

        //[TestMethod]
        //public void Write_WithMinimumGrade_PutsDataOnRightPosition()
        //{
        //    // Arrange
        //    var maximumPoints = _fixture.Create<decimal>();
        //    var standardizationFactor = _fixture.Create<StandardizationFactor>();
        //    var expectedMinimumGrade = Grade.CreateFromByte(1);
        //    var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, maximumPoints, standardizationFactor, expectedMinimumGrade);
        //    var sut = new ConfigurationBlock(configurationBlockInput);

        //    // Act
        //    sut.Write(_spreadsheetWriter);

        //    // Assert
        //    _worksheet[0, 5].Should().Be("Minimale cijfer");
        //    _worksheet[1, 5].Should().Be(expectedMinimumGrade.Value.ToString());
        //}

        //[TestMethod]
        //public void Write_WithMinimumGrade_SavesPositionOfMinimumGrade()
        //{
        //    // Arrange
        //    var maximumPoints = _fixture.Create<decimal>();
        //    var standardizationFactor = _fixture.Create<StandardizationFactor>();
        //    var minimumGrade = Grade.CreateFromByte(1);
        //    var configurationBlockInput = new ConfigurationBlock.ConfigurationBlockInput(_startpoint, maximumPoints, standardizationFactor, minimumGrade);
        //    var sut = new ConfigurationBlock(configurationBlockInput);

        //    // Act
        //    sut.Write(_spreadsheetWriter);

        //    // Assert
        //    sut.MinimumGradePosition.X.Should().Be(1);
        //    sut.MinimumGradePosition.Y.Should().Be(5);
        //}
    }
}
