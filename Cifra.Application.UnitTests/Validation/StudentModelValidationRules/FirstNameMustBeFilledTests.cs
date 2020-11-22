using AutoFixture;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Validation.QuestionModelValidationRules;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.UnitTests.Validation.StudentModelValidationRules
{
    [TestClass]
    public class FirstNameMustBeFilledTests
    {
        private Fixture _fixture;
        private Application.Validation.StudentModelValidationRules.FirstNameMustBeFilled _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _sut = new Application.Validation.StudentModelValidationRules.FirstNameMustBeFilled();
        }

        [TestMethod]
        public void Validate_ModelNull_ThrowsException()
        {
            AddStudentRequest input = null;

            Action action = () => _sut.Validate(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Validate_ValidFirstName_ReturnsNull()
        {
            AddStudentRequest input = _fixture.Build<AddStudentRequest>()
                .With(x => x.FirstName, _fixture.Create<string>())
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Validate_EmptyFirstName_ReturnsValidationMessage()
        {
            AddStudentRequest input = _fixture.Build<AddStudentRequest>()
                .With(x => x.FirstName, string.Empty)
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("FirstName");
            result.Message.Should().Be("First name is required");
        }

        [TestMethod]
        public void Validate_FirstNameNull_ReturnsValidationMessage()
        {
            AddStudentRequest input = _fixture.Build<AddStudentRequest>()
                .Without(x => x.FirstName)
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("FirstName");
            result.Message.Should().Be("First name is required");
        }

        [TestMethod]
        public void Validate_FirstNameWhitespace_ReturnsValidationMessage()
        {
            AddStudentRequest input = _fixture.Build<AddStudentRequest>()
                .With(x => x.FirstName, "    ")
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("FirstName");
            result.Message.Should().Be("First name is required");
        }
    }
}
