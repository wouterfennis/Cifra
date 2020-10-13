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
    public class NamesMustBeFilledTests
    {
        private Fixture _fixture;
        private NamesMustBeFilled _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _sut = new NamesMustBeFilled();
        }

        [TestMethod]
        public void Validate_ModelNull_ThrowsException()
        {
            AddQuestionRequest input = null;

            Action action = () => _sut.Validate(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Validate_ModelNamesNull_ThrowsException()
        {
            AddQuestionRequest input = _fixture.Build<AddQuestionRequest>()
                .Without(x => x.Names)
                .Create();

            Action action = () => _sut.Validate(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Validate_ModelNamesEmpty_ReturnsNull()
        {
            AddQuestionRequest input = _fixture.Build<AddQuestionRequest>()
                .With(x => x.Names, _fixture.CreateMany<string>(0))
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Validate_ValidNames_ReturnsNull()
        {
            AddQuestionRequest input = _fixture.Build<AddQuestionRequest>()
                .With(x => x.Names, _fixture.CreateMany<string>())
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().BeNull();
        }

        [TestMethod]
        public void Validate_InvalidName_ReturnsValidationMessage()
        {
            var names = new List<string>
            {
                _fixture.Create<string>(),
                string.Empty
            };
            AddQuestionRequest input = _fixture.Build<AddQuestionRequest>()
                .With(x => x.Names, names)
                .Create();

            Application.Validation.ValidationMessage result = _sut.Validate(input);

            result.Should().NotBeNull();
            result.Field.Should().Be("name");
            result.Message.Should().Be("Not all names are valid");
        }
    }
}
