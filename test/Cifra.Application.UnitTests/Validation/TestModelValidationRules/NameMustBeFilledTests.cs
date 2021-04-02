using AutoFixture;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Test.Requests;
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
            CreateTestRequest input = null;

            Action action = () => _sut.Validate(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Validate_ValidName_ReturnsNull()
        {
            CreateTestRequest input = _fixture.Build<CreateTestRequest>()
                .With(x => x.Name, _fixture.Create<string>())
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Validate_InvalidName_ReturnsValidationMessage()
        {
            CreateTestRequest input = _fixture.Build<CreateTestRequest>()
                .With(x => x.Name, string.Empty)
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("Name");
            result.Message.Should().Be("Name is required");
        }
    }
}
