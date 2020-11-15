using AutoFixture;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Cifra.Application.UnitTests.Models.Test
{
    [TestClass]
    public class AssignmentTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void GetMaximumPoints_WithNoQuestions_ThrowsException()
        {
            var sut = new Assignment();

            decimal result = sut.GetMaximumPoints();

            result.Should().Be(0);
        }

        [TestMethod]
        public void GetMaximumPoints_WithQuestions_ReturnsSumOfAllQuestionPoints()
        {
            var questions = new List<Question>
            {
                new Question(_fixture.CreateMany<Name>(), QuestionScore.CreateFromByte(1)),
                new Question(_fixture.CreateMany<Name>(), QuestionScore.CreateFromByte(2)),
                new Question(_fixture.CreateMany<Name>(), QuestionScore.CreateFromByte(3)),
            };
            var sut = new Assignment(Guid.NewGuid(), questions);

            decimal result = sut.GetMaximumPoints();

            result.Should().Be(6);
        }

        [TestMethod]
        public void GetMaximumQuestionNamesPerQuestion_WithNoQuestions_ReturnsMaximumOfAllQuestionNames()
        {
            var questions = new List<Question>();
            var sut = new Assignment(Guid.NewGuid(), questions);

            decimal result = sut.GetMaximumQuestionNamesPerQuestion();

            result.Should().Be(0);
        }

        [TestMethod]
        public void GetMaximumQuestionNamesPerQuestion_WithQuestions_ReturnsMaximumOfAllQuestionNames()
        {
            var expectedMaximum = 3;
            var questions = new List<Question>
            {
                new Question(_fixture.CreateMany<Name>(1), QuestionScore.CreateFromByte(1)),
                new Question(_fixture.CreateMany<Name>(2), QuestionScore.CreateFromByte(1)),
                new Question(_fixture.CreateMany<Name>(expectedMaximum), QuestionScore.CreateFromByte(1)),
            };
            var sut = new Assignment(Guid.NewGuid(), questions);

            decimal result = sut.GetMaximumQuestionNamesPerQuestion();

            result.Should().Be(expectedMaximum);
        }

        [TestMethod]
        public void AddQuestion_QuestionNull_ThrowsException()
        {
            var sut = new Assignment();

            Action action = () => sut.AddQuestion(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void AddQuestion_WithQuestion_AddsQuestionToAssignment()
        {
            Question input = _fixture.Create<Question>();
            var sut = new Assignment();

            sut.AddQuestion(input);

            sut.Questions.Should().ContainSingle();
            sut.Questions.Should().Contain(input);
        }
    }
}
