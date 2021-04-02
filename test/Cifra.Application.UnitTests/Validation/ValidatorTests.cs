using AutoFixture;
using Cifra.Application.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Cifra.Application.UnitTests.Validation
{
    [TestClass]
    public class ValidatorTests
    {
        private Fixture _fixture;
        private Mock<IValidationRule<Model>> _validationRule1;
        private Mock<IValidationRule<Model>> _validationRule2;
        private Validator<Model> _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _validationRule1 = new Mock<IValidationRule<Model>>();
            _validationRule2 = new Mock<IValidationRule<Model>>();
            var rules = new List<IValidationRule<Model>> { _validationRule1.Object, _validationRule2.Object };

            _sut = new Validator<Model>(rules);
        }

        [TestMethod]
        public void ValidateRules_WithRules_ExecutesAllRulesOnModel()
        {
            var model = _fixture.Create<Model>();

            _sut.ValidateRules(model);

            _validationRule1.Verify(x => x.Validate(model), Times.Once);
            _validationRule2.Verify(x => x.Validate(model), Times.Once);
        }

        [TestMethod]
        public void ValidateRules_WithRulesThatReturnsValidationMessage_CollectsAndReturnsValidationMessages()
        {
            var model = _fixture.Create<Model>();

            var expectedValidationMessage = _fixture.Create<ValidationMessage>();
            _validationRule1.Setup(x => x.Validate(model))
                .Returns(expectedValidationMessage);
            _validationRule2.Setup(x => x.Validate(model))
                .Returns(expectedValidationMessage);

            var result = _sut.ValidateRules(model);

            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(new[] { expectedValidationMessage, expectedValidationMessage });
        }

        public class Model
        {
        }
    }
}
