using System;
using AutoFixture;
using Cifra.Application.Models.ValueTypes;
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
        public void Equals_TwoSeperateNamesWithSameValue_AreEqual()
        {
            string input = _fixture.Create<string>();

            var name1 = Name.CreateFromString(input);
            var name2 = Name.CreateFromString(input);

            name1.Should().Equals(name2);
        }
    }
}
