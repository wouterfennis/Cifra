using Cifra.Application.Models.ValueTypes;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cifra.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using System.Linq;

namespace Cifra.Application.UnitTests.Extensions
{
    [TestClass]
    public class NameExtensionsTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void ToNames_WithValidNames_ShouldNotReturnNull()
        {
            var input = _fixture.CreateMany<string>();

            IEnumerable<Name> result = input.ToNames();

            result.Should().NotBeNull();
        }

        [TestMethod]
        public void ToNames_WithValidNames_ShouldReturnNameObjectWithSameValue()
        {
            var input = _fixture.CreateMany<string>(2);

            IEnumerable<Name> result = input.ToNames();

            result.Should().Contain(x => x.Value == input.First());
            result.Should().Contain(x => x.Value == input.ElementAt(1));
        }

        [TestMethod]
        public void ToNames_WithInvalidInput_ShouldThrowException()
        {
            List<string> input = null;

            Action action = () =>  input.ToNames();

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
