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
    public class NameMustBeFilledTests
    {
        private Fixture _fixture;
        private Application.Validation.StudentModelValidationRules.NameMustBeFilled _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _sut = new Application.Validation.StudentModelValidationRules.NameMustBeFilled();
        }

        [TestMethod]
        public void Validate_ModelNull_ThrowsException()
        {
            AddStudentRequest input = null;

            Action action = () => _sut.Validate(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Validate_ValidName_ReturnsNull()
        {
            AddStudentRequest input = _fixture.Build<AddStudentRequest>()
                .With(x => x.FullName, _fixture.Create<string>())
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Validate_InvalidName_ReturnsValidationMessage()
        {
            AddStudentRequest input = _fixture.Build<AddStudentRequest>()
                .With(x => x.FullName, string.Empty)
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("FullName");
            result.Message.Should().Be("Name is required");
        }
    }
}
