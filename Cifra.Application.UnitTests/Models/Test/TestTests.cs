using System;
using System.Linq;
using AutoFixture;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cifra.Application.UnitTests.Models.Test
{
    [TestClass]
    public class TestTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Constructor_NewClass_GeneratesId()
        {
            var result = new Application.Models.Test.Test(
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                Grade.CreateFromByte(5),
                1);

            result.Should().NotBeNull();
            result.Id.Should().NotBe(Guid.Empty);
        }

        [TestMethod]
        public void Constructor_ExistingClassWithQuestionsNull_ThrowsException()
        {
            Action action = () => new Application.Models.Test.Test(
                _fixture.Create<Guid>(),
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                Grade.CreateFromByte(5),
                null,
                1);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void AddAssignment_WithAssignment_AddsAssignmentToTest()
        {
            Application.Models.Test.Test sut = CreateDefaultTest();

            var expectedAssignment = new Assignment(1);

            sut.AddAssignment(expectedAssignment);

            sut.Assignments.Should().ContainSingle();
            sut.Assignments.Single().Should().Be(expectedAssignment);
        }

        private Application.Models.Test.Test CreateDefaultTest()
        {
            return new Application.Models.Test.Test(
                _fixture.Create<Guid>(),
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                Grade.CreateFromByte(5),
                _fixture.CreateMany<Assignment>(0).ToList(),
                1);
        }
    }
}

