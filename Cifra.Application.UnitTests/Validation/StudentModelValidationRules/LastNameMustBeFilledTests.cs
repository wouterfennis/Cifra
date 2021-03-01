using System;
using AutoFixture;
using Cifra.Application.Models.Class.Requests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cifra.Application.UnitTests.Validation.StudentModelValidationRules
{
    [TestClass]
    public class LastNameMustBeFilledTests
    {
        private Fixture _fixture;
        private Application.Validation.StudentModelValidationRules.LastNameMustBeFilled _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _sut = new Application.Validation.StudentModelValidationRules.LastNameMustBeFilled();
        }

        [TestMethod]
        public void Validate_ModelNull_ThrowsException()
        {
            AddStudentRequest input = null;

            Action action = () => _sut.Validate(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Validate_ValidLastName_ReturnsNull()
        {
            AddStudentRequest input = _fixture.Build<AddStudentRequest>()
                .With(x => x.LastName, _fixture.Create<string>())
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Validate_EmptyLastName_ReturnsValidationMessage()
        {
            AddStudentRequest input = _fixture.Build<AddStudentRequest>()
                .With(x => x.LastName, string.Empty)
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("LastName");
            result.Message.Should().Be("Last name is required");
        }

        [TestMethod]
        public void Validate_LastNameNull_ReturnsValidationMessage()
        {
            AddStudentRequest input = _fixture.Build<AddStudentRequest>()
                .Without(x => x.LastName)
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("LastName");
            result.Message.Should().Be("Last name is required");
        }

        [TestMethod]
        public void Validate_LastNameWhitespace_ReturnsValidationMessage()
        {
            AddStudentRequest input = _fixture.Build<AddStudentRequest>()
                .With(x => x.LastName, "    ")
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("LastName");
            result.Message.Should().Be("Last name is required");
        }
    }
}
