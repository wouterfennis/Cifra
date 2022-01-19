using AutoFixture;
using Cifra.Application.Models.Class.Commands;
using Cifra.Core.Models.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            AddStudentCommand input = null;

            Action action = () => _sut.Validate(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Validate_ValidLastName_ReturnsNull()
        {
            AddStudentCommand input = _fixture.Build<AddStudentCommand>()
                .With(x => x.LastName, _fixture.Create<string>())
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Validate_EmptyLastName_ReturnsValidationMessage()
        {
            AddStudentCommand input = _fixture.Build<AddStudentCommand>()
                .With(x => x.LastName, string.Empty)
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("LastName");
            result.Message.Should().Be("Last name is required");
        }

        [TestMethod]
        public void Validate_LastNameNull_ReturnsValidationMessage()
        {
            AddStudentCommand input = _fixture.Build<AddStudentCommand>()
                .Without(x => x.LastName)
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("LastName");
            result.Message.Should().Be("Last name is required");
        }

        [TestMethod]
        public void Validate_LastNameWhitespace_ReturnsValidationMessage()
        {
            AddStudentCommand input = _fixture.Build<AddStudentCommand>()
                .With(x => x.LastName, "    ")
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("LastName");
            result.Message.Should().Be("Last name is required");
        }
    }
}
