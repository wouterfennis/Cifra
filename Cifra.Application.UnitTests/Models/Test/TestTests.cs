using AutoFixture;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Application.UnitTests.Models.Test
{
    [TestClass]
    public class TestTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Constructor_NewClass_GeneratesId()
        {
            var result = new Application.Models.Test.Test(
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                Grade.CreateFromByte(5));

            result.Should().NotBeNull();
            result.Id.Should().NotBe(Guid.Empty);
        }

        [TestMethod]
        public void Constructor_ExistingClassWithQuestionsNull_ThrowsException()
        {
            Action action = () => new Application.Models.Test.Test(
                _fixture.Create<Guid>(),
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                Grade.CreateFromByte(5),
                null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void AddAssignment_WithAssignment_AddsAssignmentToTest()
        {
            Application.Models.Test.Test sut = CreateDefaultTest();

            var expectedAssignment = new Assignment(_fixture.Create<Name>(), _fixture.CreateMany<Question>(0).ToList());

            sut.AddAssignment(expectedAssignment);

            sut.Assignments.Should().ContainSingle();
            sut.Assignments.Single().Should().Be(expectedAssignment);
        }

        private Application.Models.Test.Test CreateDefaultTest()
        {
            return new Application.Models.Test.Test(
                _fixture.Create<Guid>(),
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                Grade.CreateFromByte(5),
                _fixture.CreateMany<Assignment>(0).ToList());
        }

        [TestMethod]
        public void AddQuestion_WithQuestionNull_ThrowsException()
        {
            var sut = CreateDefaultTest();

            Action action = () => sut.AddAssignment(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void GetMaximumQuestionNamesPerAssignment_WithMultipleAssignments_ReturnsLargestNumber()
        {
            int expectedResult = 3;
            var questionWithOneName = new Question(_fixture.CreateMany<Name>(1), QuestionScore.CreateFromByte(_fixture.Create<byte>()));
            var questionWithTwoNames = new Question(_fixture.CreateMany<Name>(2), QuestionScore.CreateFromByte(_fixture.Create<byte>()));
            var questionWithThreeNames = new Question(_fixture.CreateMany<Name>(3), QuestionScore.CreateFromByte(_fixture.Create<byte>()));

            var smallestAssignment = new Assignment(Name.CreateFromString(_fixture.Create<string>()));
            smallestAssignment.AddQuestion(questionWithOneName);
            var mediumAssignment = new Assignment(Name.CreateFromString(_fixture.Create<string>()));
            mediumAssignment.AddQuestion(questionWithTwoNames);
            var largestAssignment = new Assignment(Name.CreateFromString(_fixture.Create<string>()));
            largestAssignment.AddQuestion(questionWithThreeNames);

            var assignments = new List<Assignment>()
            {
                smallestAssignment,
                mediumAssignment,
                largestAssignment
            };

            var sut = new Application.Models.Test.Test(
                        _fixture.Create<Guid>(),
                        Name.CreateFromString(_fixture.Create<string>()),
                        StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                        Grade.CreateFromByte(5),
                        assignments);

            int result = sut.GetMaximumQuestionNamesPerAssignment();

            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void GetMaximumQuestionNamesPerAssignment_WithNoAssignments_ReturnsZero()
        {
            var assignments = new List<Assignment>();

            var sut = new Application.Models.Test.Test(
                        _fixture.Create<Guid>(),
                        Name.CreateFromString(_fixture.Create<string>()),
                        StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                        Grade.CreateFromByte(5),
                        assignments);

            int result = sut.GetMaximumQuestionNamesPerAssignment();

            result.Should().Be(0);
        }

        [TestMethod]
        public void GetMaximumPoints_WithMultipleAssignments_ReturnsTheSumOfAllQuestions()
        {
            int expectedResult = 6;
            var assignments = new List<Assignment>
            {
                new Assignment(Name.CreateFromString(_fixture.Create<string>()),
                _fixture.CreateMany<Question>(1).ToList()),
                new Assignment(Name.CreateFromString(_fixture.Create<string>()),
                _fixture.CreateMany<Question>(2).ToList()),
                new Assignment(Name.CreateFromString(_fixture.Create<string>()),
                _fixture.CreateMany<Question>(3).ToList())
            };

            var sut = new Application.Models.Test.Test(
                        _fixture.Create<Guid>(),
                        Name.CreateFromString(_fixture.Create<string>()),
                        StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                        Grade.CreateFromByte(5),
                        assignments);

            decimal result = sut.GetMaximumPoints();

            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void GetMaximumPoints_WithNoAssignments_ReturnsZero()
        {
            var assignments = new List<Assignment>();

            var sut = new Application.Models.Test.Test(
                        _fixture.Create<Guid>(),
                        Name.CreateFromString(_fixture.Create<string>()),
                        StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                        Grade.CreateFromByte(5),
                        assignments);

            decimal result = sut.GetMaximumPoints();

            result.Should().Be(0);
        }
    }
}
