using System;
using AutoFixture;
using Cifra.Domain.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cifra.Application.UnitTests.Models.ValueTypes
{
    [TestClass]
    public class PathTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void CreateFromString_ValidPath_ReturnsPath()
        {
            var input = _fixture.Create<string>();

            Path result = Path.CreateFromString(input).Value;

            result.Value.Should().Be(input);
        }

        [TestMethod]
        public void CreateFromString_PathIsNull_ThrowsException()
        {
            string input = null;

            Action action = () => Path.CreateFromString(input);

            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateFromString_PathIsEmpty_ThrowsException()
        {
            string input = string.Empty;

            Action action = () => Path.CreateFromString(input);

            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ImplicitFromPathToString_WithValidValue_ConvertsToString()
        {
            Path input = Path.CreateFromString(_fixture.Create<string>()).Value;

            string result = input;

            result.Should().Be(input.Value);
        }
    }
}
