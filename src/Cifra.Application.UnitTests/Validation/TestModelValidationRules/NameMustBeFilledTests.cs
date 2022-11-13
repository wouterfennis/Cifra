using AutoFixture;
using Cifra.Application.Models.Test.Commands;
using Cifra.Domain.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cifra.Application.UnitTests.Validation.TestModelValidationRules
{
    [TestClass]
    public class NameMustBeFilledTests
    {
        private Fixture _fixture;
        private Application.Validation.TestModelValidationRules.NameMustBeFilled _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _sut = new Application.Validation.TestModelValidationRules.NameMustBeFilled();
        }

        [TestMethod]
        public void Validate_ModelNull_ThrowsException()
        {
            CreateTestCommand input = null;

            Action action = () => _sut.Validate(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Validate_ValidName_ReturnsNull()
        {
            CreateTestCommand input = _fixture.Build<CreateTestCommand>()
                .With(x => x.Name, _fixture.Create<string>())
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Validate_InvalidName_ReturnsValidationMessage()
        {
            CreateTestCommand input = _fixture.Build<CreateTestCommand>()
                .With(x => x.Name, string.Empty)
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("Name");
            result.Message.Should().Be("Name is required");
        }
    }
}
