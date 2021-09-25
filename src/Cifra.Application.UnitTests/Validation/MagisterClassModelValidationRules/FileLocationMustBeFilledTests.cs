using AutoFixture;
using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Validation;
using Cifra.Application.Validation.MagisterClassModelValidationRules;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cifra.Application.UnitTests.Validation.ClassModelValidationRules
{
    [TestClass]
    public class FileLocationMustBeFilledTests
    {
        private Fixture _fixture;
        private FileLocationMustBeFilled _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _sut = new FileLocationMustBeFilled();
        }

        [TestMethod]
        public void Validate_ModelNull_ThrowsException()
        {
            CreateMagisterClassCommand input = null;

            Action action = () => _sut.Validate(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Validate_ValidFileLocation_ReturnsNull()
        {
            CreateMagisterClassCommand input = _fixture.Build<CreateMagisterClassCommand>()
                .With(x => x.MagisterFileLocation, _fixture.Create<string>())
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Validate_InvalidFileLocation_ReturnsValidationMessage()
        {
            CreateMagisterClassCommand input = _fixture.Build<CreateMagisterClassCommand>()
                .With(x => x.MagisterFileLocation, string.Empty)
                .Create();

            ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("MagisterFileLocation");
            result.Message.Should().Be("File location is required");
        }
    }
}
