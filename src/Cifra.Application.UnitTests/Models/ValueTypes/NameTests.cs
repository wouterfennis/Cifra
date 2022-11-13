using System;
using AutoFixture;
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

            Name result = Name.CreateFromString(input);

            result.Value.Should().Be(input);
        }

        [TestMethod]
        public void CreateFromString_NameIsNull_ThrowsException()
        {
            string input = null;

            Action action = () => Name.CreateFromString(input);

            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateFromString_NameIsEmpty_ThrowsException()
        {
            string input = string.Empty;

            Action action = () => Name.CreateFromString(input);

            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ImplicitFromString_WithValidValue_ConvertsToName()
        {
            string input = _fixture.Create<string>();

            Name result = input;

            result.Value.Should().Be(input);
        }

        [TestMethod]
        public void ImplicitFromNameToString_WithValidValue_ConvertsToString()
        {
            Name input = Name.CreateFromString(_fixture.Create<string>());

            string result = input;

            result.Should().Be(input.Value);
        }

        [TestMethod]
        public void ToString_WithValidValue_ReturnsString()
        {
            Name input = Name.CreateFromString(_fixture.Create<string>());

            string result = input.ToString();

            result.Should().Be(input.Value);
        }
    }
}
