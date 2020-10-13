using AutoFixture;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Validation.QuestionModelValidationRules;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.UnitTests.Validation.QuestionModelValidationRules
{
    [TestClass]
    public class TestIdMustBeFilledTests
    {
        private Fixture _fixture;
        private TestIdMustBeFilled _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _sut = new TestIdMustBeFilled();
        }

        [TestMethod]
        public void Validate_ModelNull_ThrowsException()
        {
            AddQuestionRequest input = null;

            Action action = () => _sut.Validate(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Validate_ValidTestId_ReturnsNull()
        {
            AddQuestionRequest input = _fixture.Create<AddQuestionRequest>();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Validate_InvalidTestId_ReturnsValidationMessage()
        {
            AddQuestionRequest input = _fixture.Build<AddQuestionRequest>()
                .Without(x => x.TestId)
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("TestId");
            result.Message.Should().Be("TestId is required");
        }
    }
}
