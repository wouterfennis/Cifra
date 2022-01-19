using AutoFixture;
using Cifra.Application.Models.Class.Commands;
using Cifra.Core.Models.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cifra.Application.UnitTests.Validation.ClassModelValidationRules
{
    [TestClass]
    public class NameMustBeFilledTests
    {
        private Fixture _fixture;
        private Application.Validation.ClassModelValidationRules.NameMustBeFilled _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _sut = new Application.Validation.ClassModelValidationRules.NameMustBeFilled();
        }

        [TestMethod]
        public void Validate_ModelNull_ThrowsException()
        {
            CreateClassCommand input = null;

            Action action = () => _sut.Validate(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Validate_ValidName_ReturnsNull()
        {
            CreateClassCommand input = _fixture.Build<CreateClassCommand>()
                .With(x => x.Name, _fixture.Create<string>())
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Validate_InvalidName_ReturnsValidationMessage()
        {
            CreateClassCommand input = _fixture.Build<CreateClassCommand>()
                .With(x => x.Name, string.Empty)
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("Name");
            result.Message.Should().Be("Name is required");
        }
    }
}
