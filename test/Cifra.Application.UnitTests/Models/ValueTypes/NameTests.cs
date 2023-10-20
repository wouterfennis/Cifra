using System;
using AutoFixture;
using Cifra.Domain.Validation;
using Cifra.Domain.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cifra.Application.UnitTests.Models.ValueTypes
{
    [TestClass]
    public class NameTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void CreateFromString_ValidName_ReturnsName()
        {
            var input = _fixture.Create<string>();

            Name result = Name.CreateFromString(input).Value;

            result.Value.Should().Be(input);
        }

        [TestMethod]
        public void CreateFromString_NameIsNull_ResultFails()
        {
            string input = null;

            var result = Name.CreateFromString(input);

            result.IsSuccess.Should().BeFalse();
            result.ValidationMessage.Message.Should().Be("Name cannot be null or empty");
            result.Value.Should().BeNull();
        }

        [TestMethod]
        public void CreateFromString_NameIsEmpty_ResultFails()
        {
            string input = string.Empty;

            var result = Name.CreateFromString(input);

            result.IsSuccess.Should().BeFalse();
            result.ValidationMessage.Message.Should().Be("Name cannot be null or empty");
            result.Value.Should().BeNull();
        }

        [TestMethod]
        public void ImplicitFromNameToString_WithValidValue_ConvertsToString()
        {
            Name input = Name.CreateFromString(_fixture.Create<string>()).Value;

            string result = input;

            result.Should().Be(input.Value);
        }

        [TestMethod]
        public void ToString_WithValidValue_ReturnsString()
        {
            Name input = Name.CreateFromString(_fixture.Create<string>()).Value;

            string result = input.ToString();

            result.Should().Be(input.Value);
        }
    }
}
