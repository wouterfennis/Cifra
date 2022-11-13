using AutoFixture;
using Cifra.Application.Models.Class.Commands;
using Cifra.Domain.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            AddStudentCommand input = null;

            Action action = () => _sut.Validate(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Validate_ValidFirstName_ReturnsNull()
        {
            AddStudentCommand input = _fixture.Build<AddStudentCommand>()
                .With(x => x.FirstName, _fixture.Create<string>())
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Validate_EmptyFirstName_ReturnsValidationMessage()
        {
            AddStudentCommand input = _fixture.Build<AddStudentCommand>()
                .With(x => x.FirstName, string.Empty)
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("FirstName");
            result.Message.Should().Be("First name is required");
        }

        [TestMethod]
        public void Validate_FirstNameNull_ReturnsValidationMessage()
        {
            AddStudentCommand input = _fixture.Build<AddStudentCommand>()
                .Without(x => x.FirstName)
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("FirstName");
            result.Message.Should().Be("First name is required");
        }

        [TestMethod]
        public void Validate_FirstNameWhitespace_ReturnsValidationMessage()
        {
            AddStudentCommand input = _fixture.Build<AddStudentCommand>()
                .With(x => x.FirstName, "    ")
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("FirstName");
            result.Message.Should().Be("First name is required");
        }
    }
}
